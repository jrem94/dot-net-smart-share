using System;
using System.Runtime.ConstrainedExecution;
using Client.DataTransferObjects;
using System.IO;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.Net;

namespace Client.Api
{
    public class Api
    {
        //Collect port, host, and endpoint data.
        private static readonly int PORT = 3000;
        private static readonly IPAddress HOST = IPAddress.Parse("127.0.0.1");
        private static readonly IPEndPoint ENDPOINT = new IPEndPoint(HOST, PORT);

        //Leave as empty constructor
        private Api() { }

        //Send FileObject DTO and return a response.
        private static Response SendRequest(FileObject file)
        {
            //Open client connection.
            TcpClient client = new TcpClient();
            client.Connect(ENDPOINT);

            //Serialize and send/receive data from client to server.
            XmlSerializer serializer = new XmlSerializer(typeof(FileObject));
            XmlSerializer reader = new XmlSerializer(typeof(Response));
            Response response;

            using (NetworkStream stream = new NetworkStream(client.Client))
            {
                serializer.Serialize(stream, file);
                client.Client.Shutdown(SocketShutdown.Send);
                response = (Response)reader.Deserialize(stream);
            }

            return response;
        }

        //View a file fiven a file name and password.
        public static string View(string fileName, string password)
        {
            FileObject file = new FileObject(fileName, password, true);
            Response response = SendRequest(file);
            return response.Message;
        }

        //Download a file given a file name and password.
        public static string Download(string fileName, string password)
        {
            FileObject file = new FileObject(fileName, password, false);
            Response response = SendRequest(file);
            if(response.Data != null)
            {
                File.WriteAllBytes($@"..\Downloads\{response.FileName}", response.Data);
            }
            return response.Message;
        }

        //Upload a file given the path to the file, a password, an expiration, and maximum downloads.
        public static string Upload(string path, string password, double expiration, int maxDownloads)
        {
            FileObject file = new FileObject(path, password, false, expiration, maxDownloads);
            Response response = SendRequest(file);
            return response.Message;
        }
    }
}