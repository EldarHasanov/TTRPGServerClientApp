using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DNDServerApp
{
    internal class TcpServer
    {
        private TcpListener _serverListener;
        private bool _isRunning;


        public void StartServer(int port = 0)
        {

            _serverListener = new TcpListener(IPAddress.Any, port);

            _isRunning = true;

            try
            {
                _serverListener.Start();
                ClientLoop();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        private void ClientLoop()
        {
            while (_isRunning)
            {
                TcpClient neClient = _serverListener.AcceptTcpClient();

                Thread t = new Thread(new ParameterizedThreadStart(ClientHandler));
                t.Start(neClient);
            }
        }

        private void ClientHandler(object obj)
        {
            TcpClient client = (TcpClient)obj;

            StreamWriter streamWriter = new StreamWriter(client.GetStream());
            StreamReader stteamReader = new StreamReader(client.GetStream());

            bool clientConnected = true;
            int data = -1;

            while (clientConnected)
            {
               data = stteamReader.Read();

               if (data == 0)
               {
                   streamWriter.WriteLine("Q");
                   clientConnected = false;
               }
            }
        }






    }
}
