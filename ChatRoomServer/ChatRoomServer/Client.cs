using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomServer
{
    class Client
    {
        private Socket clientsocket;

        public Client ( Socket s)
        {
             clientsocket = s;
        }



    }
}
