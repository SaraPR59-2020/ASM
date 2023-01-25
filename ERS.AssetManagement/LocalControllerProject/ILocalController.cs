using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalControllerProject
{
    public interface ILocalController
    {
        bool ReceiveData();

        void Startup();

        void StartServer();


        bool SendToAMS();
    }
}
