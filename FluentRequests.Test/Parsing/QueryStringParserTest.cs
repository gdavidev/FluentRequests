using FluentRequests.Parsing;
using FluentRequests.Test.Mock.Samples;

namespace FluentRequests.Test.Parsing;

public class QueryStringParserTest
{
    [Fact]
    public void ParseQueryString_DictionaryStringString_SerializeDictionaryIntoAQueryString()
    {
        string sampleUri = "http://example.com";
        Dictionary<string, string> queryParamsDict = new Dictionary<string, string>()
        {
          { "page", "2" },
          { "pageSize", "10" }
        };

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsDict);

        Assert.Equal("http://example.com?page=2&pageSize=10", resolvedUri);
    }

    [Fact]
    public void ParseQueryString_DictionaryStringObject_SerializeDictionaryIntoAQueryString()
    {
        string sampleUri = "http://example.com";
        Dictionary<string, object> queryParamsDict = new Dictionary<string, object>()
        {
          { "page", 2 },
          { "pageSize", 10 },
          { "term", "Produto" }
        };

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsDict);

        Assert.Equal("http://example.com?page=2&pageSize=10&term=Produto", resolvedUri);
    }

    [Fact]
    public void ParseQueryString_Object_SerializeObjectIntoAQueryString()
    {
        string sampleUri = "http://example.com";
        object queryParamsObject = new
        {
            Page = 2,
            PageSize = 10
        };

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsObject);

        Assert.Equal("http://example.com?page=2&pageSize=10", resolvedUri);
    }

    [Fact]
    public void ParseQueryString_ObjectWithCollectionProperty_DuplicateCollectionAttributeForEveryValue()
    {
        string sampleUri = "http://example.com";
        object queryParamsObject = new
        {
            Page = 2,
            PageSize = 10,
            Status = new List<string>() { "scheduled", "rescheduled", "confirmed" }
        };

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsObject);

        Assert.Equal(
          "http://example.com?page=2&pageSize=10&status=scheduled&status=rescheduled&status=confirmed",
          resolvedUri);
    }

    [Fact]
    public void ParseQueryString_NullValue_OmitQueryString()
    {
        string sampleUri = "http://example.com";
        object queryParamsObject = null!;

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsObject);

        Assert.Equal("http://example.com", resolvedUri);
    }

    [Fact]
    public void ParseQueryString_ShouldOmitEmptyCollectionProperty_OmitPropertyWithEmptyCollection()
    {
        string sampleUri = "http://example.com";
        object queryParamsObject = new QueryParamsObjectSample()
        {
            Page = 1,
            PageSize = 50,
            CustomerDocument = "123456789",
            PetName = "Toddy",
            Status = [],
            NullableString = "non-null"
        };

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsObject);

        Assert.Equal(
          "http://example.com?page=1&pageSize=50&customerDocument=123456789&petName=Toddy&nullableString=non-null",
          resolvedUri);
    }

    [Fact]
    public void ParseQueryString_ShouldOmitEmptyStringProperty_OmitPropertyWithEmptyString()
    {
        string sampleUri = "http://example.com";
        object queryParamsObject = new QueryParamsObjectSample()
        {
            Page = 1,
            PageSize = 50,
            CustomerDocument = "123456789",
            PetName = "",
            Status = ["scheduled", "active"],
            NullableString = "non-null"
        };

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsObject);

        Assert.Equal(
          "http://example.com?page=1&pageSize=50&customerDocument=123456789&nullableString=non-null&status=scheduled&status=active",
          resolvedUri);
    }

    [Fact]
    public void ParseQueryString_ShouldOmitNullProperty_OmitNullProperty()
    {
        string sampleUri = "http://example.com";
        object queryParamsObject = new QueryParamsObjectSample()
        {
            Page = 1,
            PageSize = 50,
            CustomerDocument = "123456789",
            PetName = "Toddy",
            Status = ["scheduled", "active"],
            NullableString = null
        };

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsObject);

        Assert.Equal(
          "http://example.com?page=1&pageSize=50&customerDocument=123456789&petName=Toddy&status=scheduled&status=active",
          resolvedUri);
    }

    [Fact]
    public void ParseQueryString_NoValidProperty_OmitQueryString()
    {
        string sampleUri = "http://example.com";
        object queryParamsObject = new
        {
            Page = "",
            PageSize = "",
        };

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsObject);

        Assert.Equal("http://example.com", resolvedUri);
    }

    [Fact]
    public void ParseQueryString_DateOnlyType_ConvertToUrlSafe()
    {
        string sampleUri = "http://example.com";
        object queryParamsObject = new
        {
            DateStart = new DateOnly(2025, 05, 10)
        };

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsObject);

        Assert.Equal("http://example.com?dateStart=2025-05-10", resolvedUri);
    }

    [Fact]
    public void ParseQueryString_DateTimeType_ConvertToUrlSafe()
    {
        string sampleUri = "http://example.com";
        object queryParamsObject = new
        {
            DateStart = new DateTime(2025, 05, 10, 12, 32, 52)
        };

        string resolvedUri = sampleUri + QueryStringParser.Parse(queryParamsObject);

        Assert.Equal("http://example.com?dateStart=2025-05-10T12%3a32%3a52", resolvedUri);
    }
}