using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeviceProject
{
    public interface ILocalDevice
    {
        string CreateHash();

        void Startup();

        bool SendData();
    }
}
