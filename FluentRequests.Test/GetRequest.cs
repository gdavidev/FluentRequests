using FluentRequests.Test.Mock.Samples;
using FluentRequests.Test.Mock.Samples.Login;

namespace FluentRequests.Test
{
    public class GetRequest
    {
        [Fact]
        public async Task GetRequest_Success()
        {
            var url = "https://example.com/produts";
            var query = new SamplePaginationRequest()
            {
                Page = 1,
                PageSize = 10
            };

            SampleLoginResponse result = await Requests
                .Get<SampleLoginResponse>(url)
                .WithQuery(query)
                .Catch(err => Assert.Fail());

            Assert.NotNull(result);
        }
    }
}
