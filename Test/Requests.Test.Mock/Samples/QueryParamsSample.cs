namespace Requests.Test.Mock.Samples;

public class QueryParamsObjectSample
{
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 0;
    public string CustomerDocument { get; set; } = "";
    public string PetName { get; set; } = "";
    public List<string> Status { get; set; } = [];
    public string? NullableString { get; set; } = null;
}
