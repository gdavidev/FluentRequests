using FluentRequests.Data;
using FluentRequests.Test.Mock.Samples;
using FluentRequests.Test.Mock.Samples.Login;
using FluentRequests.Test.Mock.Samples.Products;

namespace FluentRequests.Test
{
    public class PostRequest
    {
        [Fact]
        public async Task PostRequest_WithContextCreation_Success()
        {
            RequestsResult<SampleLoginResponse> result = await Requests
                .Post<SampleLoginResponse>("https://example.com/auth", context =>
                    context.Body = new SampleLoginRequest()
                    {
                        Email = "lucario@gmail.com",
                        Password = "pokemon123"
                    })
                .SendAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task PostRequest_WithBody_Success()
        {
            RequestsResult<SampleLoginResponse> result = await Requests
                .Post<SampleLoginResponse>("https://example.com/auth")
                .WithBody(new SampleLoginRequest()
                {
                    Email = "lucario@gmail.com",
                    Password = "pokemon123"
                })
                .SendAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task PostRequest_WithQuery_Success()
        {
            RequestsResult<SampleLoginResponse> result = await Requests
                .Post<SampleLoginResponse>("https://example.com/products")
                .WithQuery(new SamplePaginationRequest()
                {
                    Page = 1,
                    PageSize = 10
                })
                .SendAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task PostRequest_WithBodyAndWithQuery_Success()
        {
            RequestsResult<SampleLoginResponse> result = await Requests
                .Post<SampleLoginResponse>("https://example.com/products")
                .WithBody(new SampleProductFilterRequest()
                {
                    SearchTerm = "Shampoo"
                })
                .WithQuery(new SamplePaginationRequest()
                {
                    Page = 1,
                    PageSize = 10
                })
                .SendAsync();

            Assert.NotNull(result);
        }

        // Beyond scope
        //[Fact]
        //public async Task PostRequest_WithRouteParams_Success()
        //{
        //    var url = "https://example.com/products/{id}";
        //    var productId = 16;

        //    SampleLoginResponse result = await Requests
        //        .Post<SampleLoginResponse>(url)
        //        .WithRouteParams(productId)
        //        .Catch(err => Assert.Fail());

        //    Assert.NotNull(result);
        //}
    }
}