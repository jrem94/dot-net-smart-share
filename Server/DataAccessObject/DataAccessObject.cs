using System;
using System.Collections.Generic;
using System.Text;
using Client.DataTransferObjects;
using CsharpAssessmentSmartShare;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Server.Models;
using NpgsqlTypes;

namespace Server.DataAccessObject
{
    public class DataAccessObject
    {
        public SmartShareContext Db { get; set; }
        public FileObject File { get; set; }

        public DataAccessObject(SmartShareContext db, FileObject file)
        {
            this.Db = db;
            this.File = file;
        }
        
        public Response ViewResponse()
        {
            Response response;
            var query = from data in Db.Files
                        where data.FileName == this.File.FileName
                        select data;
            var file = query.SingleOrDefault();

            if (file != null && file.Password.Equals(File.Password))
            {
                DateTime now = DateTime.Now;
                var timeLeft = file.Expiration.Subtract(now).TotalMinutes;
                if (timeLeft > 0)
                {
                    string remainingDownloads;

                    if (file.MaxDownloads == -1)
                    {
                        remainingDownloads = "No limit";
                    }
                    else
                    {
                        remainingDownloads = $"{file.MaxDownloads - file.Downloads}";
                    }
                    return response = new Response(true, $"{file.FileName} - Expires: {file.Expiration}. Downloads Remaining: {remainingDownloads}.");
                }
                else
                {
                    Db.Files.Remove(file);
                    Db.SaveChanges();
                }
            }

            return response = new Response(false, "Cannot find requested file...");
        }

        public Response UploadResponse()
        {
            var query = from data in Db.Files
                where data.FileName == this.File.FileName
                select data;
            var dbFile = query.SingleOrDefault();

            if(dbFile != null)
            {
                return new Response(false, $"{this.File.FileName} cannot be duplicated.");
            }

            Model newFile = new Model(this.File.FileName,this.File.Password, this.File.Data, this.File.Expiration, this.File.MaxDownloads);
            Db.Add(newFile);
            try
            {
                Db.SaveChanges();
            }
            catch(Exception exception)
            {
                if (exception is DbUpdateConcurrencyException || exception is DbUpdateException)
                {
                    
                    return new Response(false, exception.Message);
                }

                throw;
            }
            return new Response(true, $"{this.File.FileName} was added.");
        }

        public Response DownloadResponse()
        {
            Response returnDto;
            var query = from data in Db.Files
                where data.FileName == this.File.FileName
                select data;
            var dbFile = query.SingleOrDefault();
            DateTime now = DateTime.Now;
   
            if(dbFile != null)
            {
                 if(dbFile.Password.Equals(File.Password))
                {
                    if(now.CompareTo(dbFile.Expiration) > 0)
                    {
                        Db.Files.Remove(dbFile);
                        Db.SaveChanges();
                    }
                    else
                    {
                        returnDto = new Response(true, "File downloaded.", dbFile.FileName, dbFile.Data);
                        dbFile.Downloads += 1;
                        if(dbFile.Downloads == dbFile.MaxDownloads)
                        {
                            Db.Files.Remove(dbFile);
                        }

                        Db.SaveChanges();
                        return returnDto;
                    }
                    
                }
            }

            return returnDto = new Response(false, "Download failed.");
        }
    }
}
