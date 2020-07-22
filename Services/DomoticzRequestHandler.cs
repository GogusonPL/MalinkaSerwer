using MalinkaSerwer.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace MalinkaSerwer.Services
{
    public class DomoticzRequestHandler : IDomoticzRequestHandler
    {
        public bool IsSmartHomeOn { get; set; }
        Timer timer;
        public DomoticzRequestHandler()
        {
            timer = new Timer();
            timer.Elapsed += CheckSmartHome;
            timer.Interval = 10000;
            timer.Start();
        }


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
                if (temperature >= 23)
                    await SetAc(true);
                else
                    await SetAc(false);
            }
        }
    }
}
