using DaveCommonsSoftware.Lib.Requests.Parsing;

namespace DaveCommonsSoftware.Lib.Requests.Builder.UriBuilder;

public class RequestsUriBuilder(string uri)
{
    private string _uri = uri;
    public object? QueryParams { get; init; }
    public object? RouteParams { get; init; }
    
    public string Build()
    {
        _uri = RouteParams is not null ? UriRouteParser.Apply(_uri, RouteParams) : _uri;
        _uri = QueryParams is not null ? QueryStringParser.Parse(QueryParams) : _uri;
        
        return _uri;
    }
}