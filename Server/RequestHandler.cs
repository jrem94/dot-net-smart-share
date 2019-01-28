using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using CsharpAssessmentSmartShare;
using Server.Models;

namespace Server
{
    class RequestHandler
    {

        public void DownloadRequest(Download download)
        {
            using (var db = new SmartShareContext())
            {
                var downloadFile = db.Downloads.Find(download.FileName);
                if (downloadFile.TotalDownloads < downloadFile.MaxDownloads)
                {
                    DirectoryInfo directory = new DirectoryInfo(@"C:\Users\jrem9\Desktop\CookSys\FTD\dotnet-assessment-smart-share-jrem94\Client\DL_UL");
                    FileInfo file = new FileInfo(downloadFile.FileName);
                    file.Open(FileMode.Create);
                    downloadFile.TotalDownloads++;
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Sorry, maximum downloads allowed reached.");
                }
            }
        }

        public void UploadRequest(Upload upload)
        {
            using (var db = new SmartShareContext())
            {
                //Set the additional information needed before adding it to the DB.
                db.Uploads.Add(upload);
                db.SaveChanges();
            }
        }
    }
}
