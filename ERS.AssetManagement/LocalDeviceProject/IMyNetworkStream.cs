using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeviceProject
{
    public interface IMyNetworkStream
    {
        NetworkStream Stream { get; set; }

        void Write(byte[] buffer, int offset, int size);

        int Read(byte[] buffer, int offset, int size);

        void Close();
    }
}
