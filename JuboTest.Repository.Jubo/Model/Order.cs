namespace JuboTest.Repository.Jubo
{
    public class Order : JuboModelBase
    {
        public string PatientNo { get; set; }

        public string Message { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public DateTimeOffset? UpdateTime { get; set; }
    }
}