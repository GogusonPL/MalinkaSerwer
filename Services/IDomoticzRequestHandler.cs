using MalinkaSerwer.Models;
using System.Threading.Tasks;

namespace MalinkaSerwer.Services
{
    public interface IDomoticzRequestHandler
    {
        public Task<DomoticzResponse> GetCurrentInfo();
        public Task<SimpleResult> SetLight(bool isWorking);
        public Task<SimpleResult> SetAc(bool isWorking);
        bool IsSmartHomeOn { get; set; }
    }
}
