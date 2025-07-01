namespace FluentRequests.Util;

public static class ObjectUtil
{
    public static Dictionary<string, object?> ToDictionary(object obj)
    {
        var properties = obj.GetType().GetProperties();
        
        return properties.ToDictionary(
            prop => prop.Name,
            prop => prop.GetValue(prop));
    }

    public static bool IsPrimitive(object obj)
    {
        var t = obj.GetType();
        return t.IsPrimitive || t.IsValueType || (t == typeof(string));
    }
}