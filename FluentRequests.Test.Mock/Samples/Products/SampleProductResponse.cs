using FluentRequests.Test.Mock.Samples._base;

namespace FluentRequests.Test.Mock.Samples.Products
{
    public class SampleProductResponse : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public float Price { get; set; } = .0f;
    }
}
