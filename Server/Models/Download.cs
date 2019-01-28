using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Server.Models
{
    public class Download
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime ExpiryTime { get; set; }
        public int MaxDownloads { get; set; }
        public int TotalDownloads { get; set; }
        public string Password { get; set; }
    }
}
