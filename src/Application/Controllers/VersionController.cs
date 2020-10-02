using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VersionController : ControllerBase
    {
        private readonly VersionOptions _versionOptions;
        
        public VersionController(IOptionsMonitor<VersionOptions> optionsDelegate)
        {
            _versionOptions = optionsDelegate.CurrentValue;
        }

        [HttpGet]
        public IActionResult GetVersion() => Ok(_versionOptions);
    }
}