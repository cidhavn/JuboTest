using Microsoft.AspNetCore.Mvc;

namespace JuboTest.Web.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}