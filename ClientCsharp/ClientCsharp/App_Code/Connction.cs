using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ClientCsharp.App_Code
{
    public class Connction
    {
        Socket server;
        public Connction(string ip,int port)
        {
            try
            {
                server = new Socket(SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ip, port);
            }
            catch (Exception err)
            {

                throw err;
            }

           
        }

    }
}
