using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Client.Api
{
    [Serializable()]
    public class SerializableOption : ISerializable
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime ExpiryTime { get; set; }
        public int MaxDownloads { get; set; }
        public int TotalDownloads { get; set; }
        public string Password { get; set; }
        public string OptionType { get; set; }

        public SerializableOption(string FileName, string Password, string OptionType)
        {
            this.FileName = FileName;
            this.Password = Password;
            this.OptionType = OptionType;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FileName", FileName);
            info.AddValue("Password", Password);
            info.AddValue("OptionType", OptionType);
        }

        public SerializableOption(SerializationInfo info, StreamingContext context)
        {
            FileName = (string) info.GetValue("FileName", typeof(string));
            Password = (string)info.GetValue("Password", typeof(string));
            OptionType = (string)info.GetValue("OptionType", typeof(string));
        }
    }
}
