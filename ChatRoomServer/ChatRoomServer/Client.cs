using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatRoomServer
{
    //client thread class
    class Client
    {
        private Socket clientSocket;
        //Core
        private Thread t;
        private byte[] data = new byte[1024];
        public Client ( Socket s)
        {
            clientSocket = s;
            // start a  thread deal with client receive

            t = new Thread(ReceiveMessage);
            t.Start();
        }

        private void ReceiveMessage()
        {
            //thread real with received info from client
            while (true)
            {
                //if disconnected ,stop
                if(clientSocket.Poll(10, SelectMode.SelectRead))
                {
                    clientSocket.Close();
                    break;//thread stopped
                }

                int length = clientSocket.Receive(data);
                string message = Encoding.UTF8.GetString(data,0,length);
                //群发给客户端
                //boardcasting
                Program.BoardcastMeseage(message);

                Console.WriteLine("received:" + message);

            }
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(data);
        }

        public bool Connected
        {
            get { return clientSocket.Connected; }
        }
    }
}
