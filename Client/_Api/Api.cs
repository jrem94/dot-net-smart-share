using System;
using System.Runtime.ConstrainedExecution;
using Client.Verbs;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Xml.Serialization;
using Client.Api;

namespace Client._Api
{
    public class Api
    {
        private const string HOST = "127.0.0.1";
        private const int PORT = 3000;

        public static void ExecuteApiProcess(SerializableOption option)
        {
            TcpClient client = new TcpClient(HOST, PORT);
            XmlSerializer serializer = new XmlSerializer(typeof(SerializableOption));

            using (TextWriter tw = new StreamWriter(@"C:\Users\jrem9\Desktop\CookSys\FTD\dotnet-assessment-smart-share-jrem94\Client\DL_UL\" + option.FileName))
            {
                //File as byte array
                option.File = File.ReadAllBytes(option.FileName); //May need to inclue absolute path here.
                //Creation Time
                option.TimeCreated = DateTime.Now;
            //Set expiration
                Console.WriteLine("When should the file expire? EX: MM/DD/YYYY HH:MM:SS");
                var input = Console.ReadLine();
                DateTime enteredDate = DateTime.Parse(input);
                option.ExpiryTime = enteredDate;
                //Set max downloads
                Console.WriteLine("How many times can this file be downloaded?");
                var maxInput = Console.ReadLine();
                int maxDownloads = Convert.ToInt32(maxInput);
                option.MaxDownloads = maxDownloads;
                //Set total downloads
                option.TotalDownloads = 0;
                serializer.Serialize(tw, option);
            }
            client.Close();
            
        }

        /// <summary>
        /// Send download request to server
        /// </summary>
        /// <param name="">TODO</param>
        /// <returns>true if request was successful and false if unsuccessful</returns>
        //public static bool Download(DownloadOptions options)
        //{
        //    SerializableOption downloadObject = new SerializableOption();
        //    downloadObject.FileName = options.FileName;
        //    downloadObject.Password = options.Password;
        //    downloadObject.OptionType = "download";
        //    return true;
        //}

        /// <summary>
        /// Send upload request to server
        /// </summary>
        /// <param name="">TODO</param>
        /// <returns>true if request was successful and false if unsuccessful</returns>
        //public static void Upload(UploadOptions options)
        //{
        //    SerializableOption uploadObject = new SerializableOption();
        //    uploadObject.FileName = options.FileName;
        //    uploadObject.Password = options.Password;
        //    uploadObject.OptionType = "upload";
        //}
    }
}