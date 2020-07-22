using MalinkaSerwer.Models;
using System.IO;
using System.Threading.Tasks;

namespace MalinkaSerwer.Services
{
    public class DomoticzRequestHandlerMock : IDomoticzRequestHandler
    {

        public async Task<DomoticzResponse> GetCurrentInfo()
        {
            var jsonData = File.ReadAllText("domoticzResponse.json");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DomoticzResponse>(jsonData);
            return result;
        }

        public async Task<SimpleResult> SetAc(bool isWorking)
        {
            var jsonData = File.ReadAllText("simple.json");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SimpleResult>(jsonData);
            result.status = $"{isWorking}";
            return result;
        }

        public async Task<SimpleResult> SetLight(bool isWorking)
        {
            var jsonData = File.ReadAllText("simple2.json");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SimpleResult>(jsonData);
            result.status = $"{isWorking}";
            return result;
        }
    }
}
