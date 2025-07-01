using System.Web;

namespace FluentRequests.Builder.UriBuilder;

public static class UriQueryParamsHandler
{
    public static string Apply(string uri, object query)
    {
        var resolvedQuery = query.GetType().GetProperties()
            .Where(p => p.GetValue(query, null) != null)
            .Select(p => 
                p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(query, null)!.ToString()))
            .Aggregate((a, b) => a + "&" + b);

        return $"{uri}?{resolvedQuery}";
    }
}