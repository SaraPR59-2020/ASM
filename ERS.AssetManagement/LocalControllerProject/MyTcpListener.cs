using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LocalControllerProject
{
    public class MyTcpListener
    {
        public TcpListener Server { get; set; }


        public MyTcpListener() 
        {
            
        }
        public MyTcpListener(IPAddress address,int port) 
        {
            Server = new TcpListener(address, port);
        }

        public virtual void Start() 
        {
            Server.Start();
        }

        public virtual TcpClient AcceptTcpClient() 
        {
            return Server.AcceptTcpClient();
        }
    }
}
