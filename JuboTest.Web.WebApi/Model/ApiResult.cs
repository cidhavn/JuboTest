namespace JuboTest.Web.WebApi
{
    public class ApiResult
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }
    }
}