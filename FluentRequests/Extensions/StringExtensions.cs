namespace FluentRequests.Extensions;

internal static class StringExtensions
{
    public static string Replace(this string str, string[] oldValues, string newValue)
    {
        return oldValues.Aggregate(str, (current, val) => current.Replace(val, newValue));
    }
}