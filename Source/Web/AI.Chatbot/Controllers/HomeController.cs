using ClassLibrary.Mvc.Services.AppSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AI.Chatbot.Controllers
{
    public class HomeController(
        ILogger<HomeController> logger,
        IAppSettingsService appSettingsService
    ) : ApplicationBaseController<HomeController>(logger, appSettingsService)
    {
        [HttpGet("/")]
        [HttpGet("Home")]
        [HttpGet("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
