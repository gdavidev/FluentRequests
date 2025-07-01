namespace FluentRequests.Extensions;

public static class StringExtensions
{
    public static string Replace(this string str, string[] oldValues, string newValue)
    {
        return oldValues.Aggregate(str, (current, val) => current.Replace(val, newValue));
    }

    public static string Insert(this string str, string start, string end, string value)
    {
        var parts = str.Split(start);
        var left = parts[0];
        var right = parts[1].Split(end)[1];
        return $"{left}{value}{right}";
    }
}