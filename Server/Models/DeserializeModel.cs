using System;
using System.Collections.Generic;
using System.Text;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Server.Models
{
    class DeserializeModel
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime ExpiryTime { get; set; }
        public int MaxDownloads { get; set; }
        public int TotalDownloads { get; set; }
        public string Password { get; set; }
        public string OptionType { get; set; }
    }
}
