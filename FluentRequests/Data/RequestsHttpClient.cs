using System.Text;
using System.Text.Json;
using FluentRequests.Builder.UriBuilder;

namespace FluentRequests.Data;

public class RequestsHttpClient(IHttpClientFactory httpClientFactory)
{
    protected HttpClient Http { get; } = httpClientFactory.CreateClient();
    
    public async Task<HttpResponseMessage> SendRequestAsync<TRequest>(RequestContext context)
    {   
        var body = context.Body is not null ? SerializeBody(context.Body) : null;
        var url = new RequestsUriBuilder(context.Url)
        {
            QueryParams = context.Query,
            RouteParams = context.RouteParams
        }.Build();
      
        var httpMessage = new HttpRequestMessage()
        {
            RequestUri = new Uri(url),
            Method = context.Method,
            Content = body
        };
        foreach (var header in context.Headers)
        {
            httpMessage.Headers.Add(header.Key, header.Value.ToString());
        }
        
        return await Http.SendAsync(httpMessage);
    }
    
    protected virtual StringContent SerializeBody(object body)
    {
        return new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json");
    }
}