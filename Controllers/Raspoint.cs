using MalinkaSerwer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MalinkaSerwer.Controllers
{
    [Route("domoticzAuthorizer/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class Raspoint : ControllerBase
    {
        private readonly ILogger<Raspoint> logger;
        private readonly IDomoticzRequestHandler domoticz;

        public Raspoint(ILogger<Raspoint> logger, IDomoticzRequestHandler domoticz)
        {
            this.logger = logger;
            this.domoticz = domoticz;
        }
        [HttpGet("[action]", Name = "GetDomoticzStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetDomoticzStatus()
        {
            var result = domoticz.GetCurrentInfo();
            return Ok(result);
        }
        [HttpPost("[action]/{mode:length(2,3)}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetLight(string mode)
        {
            bool setter = false;
            if (mode == "on")
                setter = true;

            var result = domoticz.SetLight(setter);
            return Ok(result);
        }
        [HttpPost("[action]/{mode:length(2,3)}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetAc(string mode)
        {
            bool setter = false;
            if (mode == "on")
                setter = true;

            var result = domoticz.SetAc(setter);
            return Ok(result);
        }
        [HttpPost("[action]/{mode:length(2,3)}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetSmartHome(string mode)
        {
            bool setter = false;
            if (mode == "on")
                setter = true;

            var result = domoticz.SetAc(setter);
            return Ok(result);
        }

    }
}
