using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Server.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public DateTime Expiration { get; set; }
        public int MaxDownloads { get; set; }
        public int Downloads { get; set; } = 0;
        public string Password { get; set; }

        public Model() { }

        public Model(string fileName, string password, byte[] data, DateTime expiration, int maxDownloads)
        {
            this.FileName = fileName;
            this.Data = data;
            this.Expiration = expiration;
            this.MaxDownloads = maxDownloads;
            this.Password = password;
        }
    }
}
