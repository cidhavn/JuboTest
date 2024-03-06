namespace JuboTest.Service.Management
{
    public class OrderListViewModel
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public DateTimeOffset? UpdateTime { get; set; }
    }
}