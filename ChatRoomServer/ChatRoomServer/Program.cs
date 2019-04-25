using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomServer
{
    class Program
    {
        static List<Client> clientlist = new List<Client>();



        /// <summary>
        /// boardcasting 
        /// </summary>
        /// <param name="message"></param>
        public static void BoardcastMeseage(string message)
        {
            var notConnectedList = new List<Client>();

            foreach(var client in clientlist)
            {
                Console.WriteLine("client connected"+ client.Connected);

                if (client.Connected)
                {
                    client.SendMessage(message);
                }
                else
                {
                    notConnectedList.Add(client);
                }
            }

            //移除所有断开连接的客户端
            foreach (var temp in notConnectedList)
            {
                clientlist.Remove(temp);
            }

        }
        

        static void Main(string[] args)
        {
            Socket tcpServer= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            tcpServer.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.104"), 7788));
            tcpServer.Listen(100);

            Console.WriteLine("server running.");
            while (true)
            {
                Socket clientSocket = tcpServer.Accept();
                Console.WriteLine("a client is connected !");
                //parse into CLient Obj
                Client client = new Client(clientSocket);
                //add to the list 
                clientlist.Add(client);
            }
        }

    }
}
