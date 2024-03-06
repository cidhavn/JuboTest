namespace JuboTest.Repository.Jubo
{
    public class OrderHistory : JuboModelBase
    {
        public string OrderId { get; set; }

        public string Message { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }
}