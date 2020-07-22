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
        Timer timer;
        bool IsSmartHomeOn = false;
        private readonly ILogger<Raspoint> logger;
        private readonly IDomoticzRequestHandler domoticz;

        public Raspoint(ILogger<Raspoint> logger, IDomoticzRequestHandler domoticz)
        {
            this.logger = logger;
            this.domoticz = domoticz;
            timer = new Timer();
            timer.Elapsed += CheckSmartHome;
            timer.Interval = 10000;
            timer.Start();
        }

        private async void CheckSmartHome(object sender, ElapsedEventArgs e)
        {
            if (!IsSmartHomeOn)
                return;

            int temperature = 0;
            var result = await domoticz.GetCurrentInfo();
            var temp = result.result.Where(x => x.Name == "Temperature w pokoju");
            if (temp.Count() != 0)
            {
                string tempString = temp.FirstOrDefault().Data[0].ToString() + temp.FirstOrDefault().Data[1].ToString();
                temperature = int.Parse(tempString);
                if (temperature >= 23)
                    await domoticz.SetAc(true);
                else
                    await domoticz.SetAc(false);
            }
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
        public async Task<IActionResult> SetSmartHome(string mode)
        {
            if (mode == "on")
                IsSmartHomeOn = true;
            else
                IsSmartHomeOn = false;

            return Ok();
        }

    }
}
