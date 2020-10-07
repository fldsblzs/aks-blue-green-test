using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly ApplicationOptions _applicationOptions;
        
        public ConfigController(IOptionsMonitor<ApplicationOptions> optionsDelegate)
        {
            _applicationOptions = optionsDelegate.CurrentValue;
        }

        [HttpGet]
        public IActionResult GetConfig() => Ok(_applicationOptions);
    }
}