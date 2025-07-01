using System.Net;
using FluentRequests.Test.Mock.Samples;
using FluentRequests.Test.Mock.Samples.Login;

namespace FluentRequests.Test
{
    public class GetRequest
    {
        // [Fact]
        // public async Task GetRequest_Success()
        // {
        //     SampleLoginResponse result = await Requests
        //         .Get<SampleLoginResponse>("https://example.com/produts")
        //         .WithQuery(new SamplePaginationRequest()
        //         {
        //             Page = 1,
        //             PageSize = 10
        //         })
        //         .OnStatusCode(HttpStatusCode.BadRequest, )
        //         .Catch(err => Assert.Fail())
        //         .SendAsync();
        //
        //     Assert.NotNull(result);
        // }
    }
}
