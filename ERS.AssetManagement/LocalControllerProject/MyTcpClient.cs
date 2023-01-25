using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LocalControllerProject
{
    public class MyTcpClient
    {
        public MyTcpClient() 
        {
        
        }


        public TcpClient TcpClient { get; set; }


        public virtual NetworkStream GetStream() 
        {
            return TcpClient.GetStream();
        }
    }
}
