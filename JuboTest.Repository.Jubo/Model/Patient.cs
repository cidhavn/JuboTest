namespace JuboTest.Repository.Jubo
{
    public class Patient : JuboModelBase
    {
        public string No { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }
}