namespace Requests.Test.Mock.Samples._base
{
    public abstract class BaseAuditable
    {
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
