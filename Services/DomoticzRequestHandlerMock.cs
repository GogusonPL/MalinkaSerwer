using MalinkaSerwer.Models;
using System.IO;
using System.Timers;

namespace MalinkaSerwer.Services
{
    public class DomoticzRequestHandlerMock : IDomoticzRequestHandler
    {
        Timer timer;
        public DomoticzRequestHandlerMock()
        {
            timer = new Timer();
            timer.Elapsed += CheckSmartHome;
            timer.Interval = 10000;
            timer.Start();

        }

        private void CheckSmartHome(object sender, ElapsedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public DomoticzResponse GetCurrentInfo()
        {
            var jsonData = File.ReadAllText("domoticzResponse.json");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DomoticzResponse>(jsonData);
            return result;
        }

        public SimpleResult SetAc(bool isWorking)
        {
            var jsonData = File.ReadAllText("simple.json");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SimpleResult>(jsonData);
            result.status = $"{isWorking}";
            return result;
        }

        public SimpleResult SetLight(bool isWorking)
        {
            var jsonData = File.ReadAllText("simple2.json");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SimpleResult>(jsonData);
            result.status = $"{isWorking}";
            return result;
        }
    }
}
