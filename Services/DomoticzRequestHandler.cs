using MalinkaSerwer.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MalinkaSerwer.Services
{
    public class DomoticzRequestHandler : IDomoticzRequestHandler
    {
        public async Task<DomoticzResponse> GetCurrentInfo()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, DomoticzEndpoints.GetDataEndpoint);
                var response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DomoticzResponse>(jsonString);
                }
                return null;
            }
        }

        public async Task<SimpleResult> SetAc(bool isWorking)
        {
            string endpoint = "";
            if (isWorking)
                endpoint = DomoticzEndpoints.AcOnEndpoint;
            else
                endpoint = DomoticzEndpoints.AcOffEndpoint;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                var response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SimpleResult>(jsonString);
                }
                return null;
            }
        }

        public async Task<SimpleResult> SetLight(bool isWorking)
        {
            string endpoint = "";
            if (isWorking)
                endpoint = DomoticzEndpoints.LighOnEndpoint;
            else
                endpoint = DomoticzEndpoints.LightOffEndpoint;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                var response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SimpleResult>(jsonString);
                }
                return null;
            }
        }
    }
}
