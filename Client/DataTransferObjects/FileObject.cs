using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;

namespace Client.DataTransferObjects
{
    [Serializable()] 
    public class FileObject
    {
        public string Password { get; set; }
        public byte[] Data { get; set; }
        public DateTime Expiration { get; set; }
        public int MaxDownloads { get; set; }
        public string FileName { get; set; }
        public bool View { get; set; }

        public FileObject() { }

        public FileObject(string path, string password, bool view)
        {
            this.Password = password;
            string[] strings = path.Split('/');
            this.FileName = strings[strings.Length - 1];
            this.View = view;
        }

        public FileObject(string path, string password, bool view, double expiration, int maxDownloads)
        {
            this.Password = password;
            this.Data = File.ReadAllBytes(path);
            this.Expiration = DateTime.Now.AddMinutes(expiration);
            this.MaxDownloads = maxDownloads;
            this.FileName = Path.GetFileName(path);
            this.View = view;
        }

        public bool HasData()
        {
            if (this.Data != null)
            {
                return true;
            }

            return false;
        }
        
    }
}
