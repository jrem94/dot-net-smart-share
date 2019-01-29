using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Client.DataTransferObjects;
using CsharpAssessmentSmartShare;
using Microsoft.EntityFrameworkCore;
using Server.DataAccessObject;

namespace Server.RequestHandler
{
    public class RequestHandler
    {

        public static void HandleClientRequest(TcpClient client) 
        {
            Response response = new Response(false, "Failed response.");
            using (NetworkStream stream = client.GetStream())
            {
                //Deserialize the file from the stream.
                XmlSerializer reader = new XmlSerializer(typeof(FileObject));
                FileObject file = (FileObject)reader.Deserialize(stream);

                using (SmartShareContext db = new SmartShareContext())
                {
                    DataAccessObject.DataAccessObject data = new DataAccessObject.DataAccessObject(db, file);

                    //Check file to determine how to handle.
                    if (!(file.Data == null))
                    {
                        response = data.UploadResponse();
                    }
                    else if(file.View)
                    {
                        response = data.ViewResponse();
                    }
                    else
                    {
                        response = data.DownloadResponse();
                    }
                }
                //Respond to client and shutdown.
                XmlSerializer serializer = new XmlSerializer(typeof(Response));
                serializer.Serialize(stream, response);
                client.Client.Shutdown(SocketShutdown.Both);
            }
        }
    }
}
