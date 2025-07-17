using System.Collections;
using System.Reflection;
using System.Web;

namespace DaveCommonsSoftware.Lib.Requests.Parsing;

/// <summary>
/// Provides utility methods to convert objects or dictionaries 
/// into URL query string format and append them to a URI.
/// </summary>
internal class QueryStringParser
{
    private record PropertyEntry(string Name, object Value);

    /// <summary>
    /// Appends query parameters to a URI from either an object or a dictionary.
    /// </summary>
    /// <param name="uri">The base URI.</param>
    /// <param name="queryObject">
    /// An object or dictionary containing the query parameters.
    /// </param>
    /// <returns>The URI with the appended query string.</returns>
    public static string Parse(object queryObject)
    {
        return queryObject switch
        {
            null => string.Empty,
            IDictionary queryDict when queryDict.Count == 0 => string.Empty,
            IDictionary queryDict when queryDict.Count > 0 => ParseDictionaryAsQueryString(queryDict) is not "" and var str ? $"?{str}" : "",
            _ => ParseObjectAsQueryString(queryObject) is not "" and var str ? $"?{str}" : "",
        };
    }

    /// <summary>
    /// Converts a POCO object into a URL query string.
    /// Supports collections by flattening them into repeated parameters.
    /// </summary>
    /// <param name="queryObject">The object to convert.</param>
    /// <returns>The URL query string representation of the object.</returns>
    private static string ParseObjectAsQueryString(object queryObject)
    {
        // Collect property Name and Value into a object of anonymous type
        List<PropertyEntry> props = queryObject.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(prop => new PropertyEntry(prop.Name, prop.GetValue(queryObject, null)!))
            .Where(prop => !PropertyShouldBeOmited(prop))
            .ToList();

        // Filter out properties with a collection type and add them
        //    to the list with repeated names (Flatten the list)
        List<PropertyEntry> collections = props.Where(p => p.Value is IEnumerable<object>).ToList();
        foreach (PropertyEntry item in collections)
        {
            var flattendProps = ((IEnumerable<object>)item.Value)
              .Select(val => new PropertyEntry(item.Name, val))
              .Where(prop => !PropertyShouldBeOmited(prop));

            if (flattendProps.Any())
                props.AddRange(flattendProps);

            props.Remove(item);
        }

        // Transform into a query string and return the results
        List<string> parameters = [];
        foreach (PropertyEntry entry in props)
        {
            string encodedName = HttpUtility.UrlEncode(PascalCaseToCamelCase(entry.Name.ToString()));
            string encodedValue = EncodeValue(entry.Value);
            parameters.Add($"{encodedName}={encodedValue}");
        }

        if (parameters.Count != 0)
            return parameters.Aggregate((a, b) => $"{a}&{b}");
        return "";
    }

    /// <summary>
    /// Converts a dictionary into a URL query string.
    /// </summary>
    /// <param name="queryDict">The dictionary containing query parameters.</param>
    /// <returns>The URL query string representation of the dictionary.</returns>
    private static string ParseDictionaryAsQueryString(IDictionary queryDict)
    {
        // Transform into a query string and return the results
        List<string> parameters = [];
        foreach (DictionaryEntry entry in queryDict)
        {
            string encodedKey = HttpUtility.UrlEncode(entry.Key.ToString()!);
            string encodedValue = EncodeValue(entry.Value);
            parameters.Add($"{encodedKey}={encodedValue}");
        }

        if (parameters.Count != 0)
            return parameters.Aggregate((a, b) => $"{a}&{b}");
        return "";
    }

    /// <summary>
    /// Returns true if the property contains a invalid value to be parsed into a query string,
    /// like null, empty collection or empty strings
    /// </summary>
    /// <param name="prop">The property information of the object</param>
    /// <returns>A bool representing if the property should be omited in the query string or not.</returns>
    private static bool PropertyShouldBeOmited(PropertyEntry prop)
    {
        if (prop.Value is null)
            return true;
        if (prop.Value is string str)
            return string.IsNullOrWhiteSpace(str);
        if (prop.Value is IEnumerable<object> collection)
            return !collection.Any();
        return false;
    }

    /// <summary>
    /// Converts a PascalCase string into camelCase.
    /// </summary>
    /// <param name="str">The input PascalCase string.</param>
    /// <returns>The converted camelCase string.</returns>
    private static string PascalCaseToCamelCase(string str)
    {
        return str.Substring(0, 1).ToLower() + str.Substring(1);
    }

    /// <summary>
    /// Converts a param value to string in a URL safe way.
    /// </summary>
    /// <param name="value">The value of type object.</param>
    /// <returns>The value as a string.</returns>
    private static string EncodeValue(object? value)
    {
        return value switch
        {
            null => "",
            DateTime date => HttpUtility.UrlEncode(date.ToString("s")) ?? "", // "s" = sortable date/time. Encoded because it contains ":"
            DateOnly date => date.ToString("yyyy-MM-dd"),
            _ => value.ToString() is not null and var str ? HttpUtility.UrlEncode(str) : ""
        };
    }
}