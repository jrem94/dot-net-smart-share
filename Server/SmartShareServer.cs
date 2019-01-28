using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Serialization;
using Server.Models;

namespace Server
{
    class Program
    {

        static void Main(string[] args)
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 3000);

                while (true)
                {
                    listener.Start();
                    TcpClient client = listener.AcceptTcpClient();
                    //from client to server
                    XmlSerializer deserializer = new XmlSerializer(typeof(DeserializeModel));
                    NetworkStream stream = client.GetStream();
                    TextReader reader = new StreamReader(stream);
                    object obj = deserializer.Deserialize(reader);
                    var model = (DeserializeModel)obj;
                    if (model.OptionType == "upload")
                    {
                        Upload upload = new Upload();
                        upload.FileName = model.FileName;
                        upload.Password = model.Password;
                        upload.File = model.File;
                        upload.TimeCreated = model.TimeCreated;
                        upload.ExpiryTime = model.ExpiryTime;
                        upload.MaxDownloads = model.MaxDownloads;
                        upload.TotalDownloads = model.TotalDownloads;
                        RequestHandler rh = new RequestHandler();
                        rh.UploadRequest(upload);

                    }
                    else if (model.OptionType == "download")
                    {
                        Download download = new Download();
                        download.FileName = model.FileName;
                        download.Password = model.Password;
                        download.File = model.File;
                        download.TimeCreated = model.TimeCreated;
                        download.ExpiryTime = model.ExpiryTime;
                        download.MaxDownloads = model.MaxDownloads;
                        download.TotalDownloads = model.TotalDownloads;
                        RequestHandler rh = new RequestHandler();
                        rh.DownloadRequest(download);

                    }
                    reader.Close();

                    client.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }
    }
}