using System.Web;

namespace FluentRequests.Builder.UriBuilder;

public class RequestsUriBuilder(string uri)
{
    private string _uri = uri;
    public object? QueryParams { get; init; }
    public object? RouteParams { get; init; }
    
    public string Build()
    {
        _uri = RouteParams is not null ? UriRouteParamsHandler.Apply(_uri, RouteParams) : _uri;
        _uri = QueryParams is not null ? UriQueryParamsHandler.Apply(_uri, QueryParams) : _uri;
        
        return _uri;
    }
    
    
}