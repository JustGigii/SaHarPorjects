using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ClientCsharp.App_Code
{
    public class Connction
    {
        TcpClient server;
        NetworkStream stream;
        public Connction(string ip, int port)
        {
            try
            {
                server = new TcpClient(ip, port);
                stream = server.GetStream();
            }
            catch (Exception err)
            {

                throw err;
            }


        }
        public void Send(string message)
        {
            try
            {
                // send name to server
                byte[] buf;
                // append newline as server expects a line to be read
                buf = Encoding.UTF8.GetBytes(message + "\n");

                
                stream.Write(buf, 0, message.Length + 1);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public string Recv()
        {
            try
            {
                // read xml from server
                byte[] buf;
                buf = new byte[10000];
                stream.Read(buf, 0, 10000);
                return Encoding.UTF8.GetString(buf);
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public void Close()
        {
            try
            {
                // Close Socket using  
                // the method Close() 
                stream.Close();
                server.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
