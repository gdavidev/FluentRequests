using DaveCommonsSoftware.Lib.Requests.Builder.UriBuilder;
using System.Text;
using System.Text.Json;

namespace DaveCommonsSoftware.Lib.Requests.Data;

public class RequestsHttpClient()
{
    public async Task<RequestsResult<TResponse>> SendRequestAsync<TResponse>(RequestContext context)
    {
        var body = context.Body is not null ? SerializeRequestBody(context.Body) : null;
        var uriBuilder = new RequestsUriBuilder(context.Uri)
        {
            QueryParams = context.Query,
            RouteParams = context.RouteParams
        };
        var uri = uriBuilder.Build();

        var httpMessage = new HttpRequestMessage()
        {
            RequestUri = new Uri(uri),
            Method = context.Method,
            Content = body
        };

        foreach (var header in context.Headers)
        {
            httpMessage.Headers.Add(header.Key, header.Value.ToString());
        }

        var response = await context.HttpRequestClient!.SendAsync(httpMessage);
        var responseContent = await response.Content.ReadAsStringAsync();

        return new RequestsResult<TResponse>()
        {
            Context = context,
            Data = DeserializeResponseBody<TResponse>(responseContent),
            StatusCode = response.StatusCode,
            FinalUri = uri
        };
    }

    protected virtual StringContent SerializeRequestBody(object body)
    {
        return new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json");
    }

    protected virtual TResponse DeserializeResponseBody<TResponse>(string responseContent)
    {
        return JsonSerializer.Deserialize<TResponse>(responseContent)!;
    }
}