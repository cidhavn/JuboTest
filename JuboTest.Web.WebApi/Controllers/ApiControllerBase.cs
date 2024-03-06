using Microsoft.AspNetCore.Mvc;

namespace JuboTest.Web.WebApi.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        public ApiControllerBase()
        { }

        protected ApiResult<T> GetResult<T>(Func<T> func)
        {
            var result = new ApiResult<T>();

            if (ModelState.IsValid)
            {
                try
                {
                    result.Data = func.Invoke();
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Message = e.Message;
                }
            }
            else
            {
                var errorMsgs = new List<string>();

                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count == 0) continue;

                    errorMsgs.AddRange(state.Value.Errors.Select(x => x.ErrorMessage).ToArray());
                }

                result.Success = false;
                result.Message = string.Join(",", errorMsgs);
            }

            return result;
        }

        protected ApiResult GetResult(Action action)
        {
            var result = new ApiResult();

            if (ModelState.IsValid)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Message = e.Message;
                }
            }
            else
            {
                var errorMsgs = new List<string>();

                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count == 0) continue;

                    errorMsgs.AddRange(state.Value.Errors.Select(x => x.ErrorMessage).ToArray());
                }

                result.Success = false;
                result.Message = string.Join(",", errorMsgs);
            }

            return result;
        }
    }
}