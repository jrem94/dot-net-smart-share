using Server.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.Threading;
using Client.DataTransferObjects;
using CsharpAssessmentSmartShare;
using Server.DataAccessObject;

namespace Server
{
    class Program
    {
        //Main is technically a thread.
        static void Main(string[] args)
        {
            //Collect port, cost and server data.
            var PORT = 3000;
            var HOST = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(HOST, PORT);

            //Start the server - listening for client connections.
            server.Start();

            while (true)
            {
                Console.WriteLine("Waiting for connection...");
                try
                {
                    //Accept connection to client.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Successful handshake: Connected.");

                    //Pass client to request handler.
                    new Thread(() => RequestHandler.RequestHandler.HandleClientRequest(client)).Start();
                }
                catch(SocketException exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }
    }
}