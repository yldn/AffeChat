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
