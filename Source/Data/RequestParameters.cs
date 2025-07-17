namespace DaveCommonsSoftware.Lib.Requests.Data;

internal class RequestParameters
{
    public object? Body { get; set; } = null;
    public object? Query { get; set; } = null;
    public object? Form { get; set; } = null;
    public object? RouteParams { get; set; } = null;
}
