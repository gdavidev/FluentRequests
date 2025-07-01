namespace FluentRequests.Exceptions;

public class RequestBuildException(string message, string buildStep) : Exception(message)
{
    public string BuildStep { get; set; } = buildStep;
}