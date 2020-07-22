using MalinkaSerwer.Models;

namespace MalinkaSerwer.Services
{
    public interface IDomoticzRequestHandler
    {
        public DomoticzResponse GetCurrentInfo();
        public SimpleResult SetLight(bool isWorking);
        public SimpleResult SetAc(bool isWorking);

    }
}
