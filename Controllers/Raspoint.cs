using MalinkaSerwer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

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
        public async Task<IActionResult> GetDomoticzStatus()
        {
            var result = await domoticz.GetCurrentInfo();
            return Ok(result);
        }
        [HttpPost("[action]/{mode:length(2,3)}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetLight(string mode)
        {
            bool setter = false;
            if (mode == "on")
                setter = true;

            var result = await domoticz.SetLight(setter);
            return Ok(result);
        }
        [HttpPost("[action]/{mode:length(2,3)}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetAc(string mode)
        {
            bool setter = false;
            if (mode == "on")
                setter = true;

            var result = await domoticz.SetAc(setter);
            return Ok(result);
        }
        [HttpPost("[action]/{mode:length(2,3)}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetSmartHome(string mode)
        {
            if (mode == "on")
                domoticz.IsSmartHomeOn = true;
            else
                domoticz.IsSmartHomeOn = false;

            return Ok();
        }

    }
}
