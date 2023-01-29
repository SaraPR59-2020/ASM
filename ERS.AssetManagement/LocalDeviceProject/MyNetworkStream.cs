using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeviceProject
{
    public class MyNetworkStream : IMyNetworkStream
    {   
        public NetworkStream Stream { get; set; }


        public virtual int Read(byte[] buffer, int offset, int size)
        {
            return Stream.Read(buffer,  offset,  size);
        }

        public virtual void Write(byte[] buffer, int offset, int size)
        {
            Stream.Write(buffer, offset, size);
        }

        public virtual void Close() 
        {
            Stream.Close();
        }
    }
}
