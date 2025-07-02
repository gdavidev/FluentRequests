using FluentRequests.Exceptions;
using FluentRequests.Util;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web;

namespace FluentRequests.Parsing;

internal class UriRouteParser
{
    private record ParamEntry(string Name, object Value);

    public static string Apply(
        [StringSyntax(StringSyntaxAttribute.Uri)] string uri,
        object routeParamsObject)
    {
        if (ObjectUtil.IsPrimitive(routeParamsObject))
            return FillInRouteParamsAsPrimitive(uri, routeParamsObject);
        if (routeParamsObject is IDictionary routeParamsDictionary)
        {
            if (routeParamsDictionary.Count > 0)
                return uri;
            return FillInRouteParamsAsDictionary(uri, routeParamsDictionary);
        }        

        return FillInRouteParamsAsObjectInstance(uri, routeParamsObject);
    }    

    private static string FillInRouteParamsAsPrimitive(string uri, object routeParams)
    {
        var param = routeParams.ToString();
        var result = !string.IsNullOrEmpty(param) ? InsertNextParameter(uri, HttpUtility.UrlEncode(param)) : uri;

        if (result.Contains('{')) // parameter inserted but still contains a '{' character
            throw new RequestBuildException(
                $"Object primitive of type \"{routeParams.GetType().Name}\" doesn't fill in all params in uri format \"{uri}\".",
                "Router parameters insertion");

        return result;
    }

    private static string FillInRouteParamsAsObjectInstance(string uri, object routeParams)
    {
        // Collect property Name and Value into a PropertyEntry object
        List<ParamEntry> props = routeParams.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(prop => new ParamEntry(prop.Name, prop.GetValue(routeParams, null)))
            .ToList();

        return FillInRouteParams(uri, props, routeParams);
    }

    private static string FillInRouteParamsAsDictionary(string uri, IDictionary routeParams)
    {
        // Transform into a query string and return the results
        List<ParamEntry> props = [];
        foreach (DictionaryEntry entry in routeParams)
        {
            string name = entry.Key.ToString()!;
            string encodedValue = HttpUtility.UrlEncode(entry.Value?.ToString() ?? "");
            props.Add(new ParamEntry(name, encodedValue));
        }

        return FillInRouteParams(uri, props, routeParams);
    }

    private static string FillInRouteParams(string uri, List<ParamEntry> props, object originalObject)
    {
        while (uri.Contains('{'))
        {
            var (paramStart, paramEnd) = GetNextRouteParamPosition(uri);
            var paramName = uri.Substring(paramStart + 1, (paramEnd - paramStart) - 1);

            if (paramName.Contains(':'))
                paramName = paramName.Substring(0, paramName.IndexOf(":"));

            var prop = props.FirstOrDefault(p => p.Name.Equals(paramName, StringComparison.InvariantCultureIgnoreCase));

            if (prop is null)
                throw new RequestBuildException(
                    $"Object of type \"{originalObject.GetType().Name}\" doesn't match the uri format \"{uri}\", a param named \"{paramName}\" is missing.",
                    "Router parameters insertion");

            uri = InsertNextParameter(uri, HttpUtility.UrlEncode(prop.Value.ToString()!));
        }

        return uri;
    }

    private static (int, int) GetNextRouteParamPosition(string uri) => (uri.IndexOf('{'), uri.IndexOf('}'));

    private static string InsertNextParameter(string uri, string value)
    {
        var startIndex = uri.IndexOf('{');
        var endIndex = uri.IndexOf('}');

        var left = uri.Substring(0, startIndex);
        var right = uri.Substring(endIndex + 1, (uri.Length - endIndex) - 1);

        return $"{left}{value}{right}";
    }
}
