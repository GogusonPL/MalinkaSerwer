using MalinkaSerwer.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
namespace MalinkaSerwer.Services
{
    public class DomoticzRequestHandlerMock : IDomoticzRequestHandler
    {
        public bool IsSmartHomeOn { get; set; }
        public bool IsAcOn { get; set; }
        public bool IsLightOn { get; set; }
        System.Timers.Timer timer;
        public DomoticzRequestHandlerMock()
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += CheckSmartHome;
            timer.Interval = 5000;
            timer.Start();
        }
        public async Task<DomoticzResponse> GetCurrentInfo()
        {
            var jsonData = File.ReadAllText("domoticzResponse.json");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DomoticzResponse>(jsonData);
            return result;
        }

        public async Task<SimpleResult> SetAc(bool isWorking)
        {
            IsAcOn = isWorking;
            var jsonData = File.ReadAllText("simple.json");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SimpleResult>(jsonData);
            result.status = $"{isWorking}";
            return result;
        }

        public async Task<SimpleResult> SetLight(bool isWorking)
        {
            IsLightOn = isWorking;
            var jsonData = File.ReadAllText("simple2.json");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SimpleResult>(jsonData);
            result.status = $"{isWorking}";
            return result;
        }
        private async void CheckSmartHome(object sender, ElapsedEventArgs e)
        {
            if (!IsSmartHomeOn)
                return;

            int temperature = 0;
            var result = await GetCurrentInfo();
            var temp = result.result.Where(x => x.Name == "Temperature w pokoju");
            if (temp.Count() != 0)
            {
                string tempString = temp.FirstOrDefault().Data[0].ToString() + temp.FirstOrDefault().Data[1].ToString();
                temperature = int.Parse(tempString);
                await CheckTemperatureAndReact(temperature);
            }
        }

        public async Task CheckTemperatureAndReact(int temperature)
        {
                if (temperature >= 23)
                    await SetAc(true);
                else
                    await SetAc(false);
        }
    }
}
