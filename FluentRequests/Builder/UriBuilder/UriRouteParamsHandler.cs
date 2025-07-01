using FluentRequests.Extensions;
using FluentRequests.Util;

namespace FluentRequests.Builder.UriBuilder;

public static class UriRouteParamsHandler
{
    public static string Apply(string uri, object routeParams)
    {
        if (ObjectUtil.IsPrimitive(routeParams))
        {
            return FillInRouteParamsAsPrimitive(uri, routeParams);
        }
        if (routeParams is IEnumerable<object> or ICollection<object>)
        {
            return FillInRouteParamsAsList(uri, routeParams);    
        }
        
        return FillInRouteParamsAsObjectInstance(uri, routeParams);
    }

    private static (int, int) GetNextRouteParamPosition(string uri) => 
        (uri.IndexOf('{'), uri.IndexOf('}'));

    private static string FillInRouteParamsAsPrimitive(string uri, object routeParams)
    {
        var param = routeParams.ToString();
        return !string.IsNullOrEmpty(param) ? uri.Insert("{", "}", param) : uri;
    }

    private static string FillInRouteParamsAsObjectInstance(string uri, object routeParams)
    {
        var paramsList = ObjectUtil.ToDictionary(routeParams);
        var paramListKeyNames = paramsList.Keys
            .Select(k => new KeyValuePair<string, string>(k.ToLower(), k))
            .ToDictionary();
        
        while (uri.Contains('{'))
        {
            var (paramStart, paramEnd) = GetNextRouteParamPosition(uri);
            var paramName = uri.Substring(paramStart, (paramEnd - paramStart) + 1);
            
            if (!paramListKeyNames.ContainsKey(paramName) && paramsList[paramName] is not null)
                uri = uri.Insert("{", "}", paramsList[paramName]!.ToString()!);
        }

        return uri;
    }

    private static string FillInRouteParamsAsList(string uri, object routeParams)
    {
        throw new NotImplementedException();
    }
}