using DaveCommonsSoftware.Lib.Requests.Parsing;

namespace Requests.Test.Parsing;

public class UriRouteParserTest
{
    [Fact]
    public void ApplyRouteParams_ValueOfTypeLong_Success()
    {
        var sampleUrl = "https://example.com/products/{id}";
        var sampleValue = 2L;

        var result = UriRouteParser.Apply(sampleUrl, sampleValue);

        Assert.Equal("https://example.com/products/2", result);
    }

    [Fact]
    public void ApplyRouteParams_ValueOfTypeString_Success()
    {
        var sampleUrl = "https://example.com/products/{search}";
        var sampleValue = "soap";

        var result = UriRouteParser.Apply(sampleUrl, sampleValue);

        Assert.Equal("https://example.com/products/soap", result);
    }

    [Fact]
    public void ApplyRouteParams_MultipleValues_Success()
    {
        var sampleUrl = "https://example.com/products/{search}/{page}";
        var sampleSearchTerm = "soap";
        var samplePageNumber = 2;

        var result = UriRouteParser.Apply(sampleUrl, new { Search = sampleSearchTerm, Page = samplePageNumber });

        Assert.Equal("https://example.com/products/soap/2", result);
    }

    [Fact]
    public void ApplyRouteParams_ValueOfTypeLongWithNotation_Success()
    {        
        var sampleUrl = "https://example.com/products/{id:long}";
        var sampleValue = 2L;

        var result = UriRouteParser.Apply(sampleUrl, sampleValue);

        Assert.Equal("https://example.com/products/2", result);
    }

    [Fact]
    public void ApplyRouteParams_ValueOfTypeStringWithNotation_Success()
    {
        var sampleUrl = "https://example.com/products/{search:string}";
        var sampleValue = "soap";

        var result = UriRouteParser.Apply(sampleUrl, sampleValue);

        Assert.Equal("https://example.com/products/soap", result);
    }

    [Fact]
    public void ApplyRouteParams_MultipleValuesWithNotation_Success()
    {
        var sampleUrl = "https://example.com/products/{search:string}/{page:int}";
        var sampleSearchTerm = "soap";
        var samplePageNumber = 2;

        var result = UriRouteParser.Apply(sampleUrl, new { Search = sampleSearchTerm, Page = samplePageNumber });

        Assert.Equal("https://example.com/products/soap/2", result);
    }
}
