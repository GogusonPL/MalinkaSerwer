using MalinkaSerwer.Models;
using System.IO;

namespace MalinkaSerwer.Services
{
    public class DomoticzRequestHandlerMock : IDomoticzRequestHandler
    {
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
