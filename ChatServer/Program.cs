using ChatServer.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    class Program
    {
        static List<Client> _users;
        static TcpListener _listener;
        static void Main(string[] args)
        {
            _users = new List<Client>();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            _listener.Start();


            while (true)
            {
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);

                BrodcastConnection();
            }
        }

        static void BrodcastConnection()
        {
            foreach (var user in _users) 
            {
                foreach(var usr in _users)
                {
                    var broadcastPacket = new PacketBuilder();
                    broadcastPacket.WriteOpCode(1);
                    broadcastPacket.WriteString(usr.Username);
                    broadcastPacket.WriteString(usr.UID.ToString());
                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());

                }
            }
        }

        public static void BroadcastMessage(string message)
        {
            foreach(var user in _users) 
            {
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(5);
                msgPacket.WriteString(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }

        public static void BroadcastDisconnect(string uid)
        {
            var disconnectedUser = _users.Where(x => x.UID.ToString() == uid).FirstOrDefault();
            foreach (var user in _users)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteOpCode(10);
                broadcastPacket.WriteString(uid);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                
            }
            BroadcastMessage($"[{disconnectedUser.Username}] Disconnected!");
        }
    }
}