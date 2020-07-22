using MalinkaSerwer.Models;
using System;

namespace MalinkaSerwer.Services
{
    public class DomoticzRequestHandler : IDomoticzRequestHandler
    {
        public DomoticzResponse GetCurrentInfo()
        {
            throw new NotImplementedException();
        }

        public SimpleResult SetAc(bool isWorking)
        {
            throw new NotImplementedException();
        }

        public SimpleResult SetLight(bool isWorking)
        {
            throw new NotImplementedException();
        }
    }
}
