using System;
using System.Collections.Generic;
using System.Text;

namespace Client.DataTransferObjects
{
    [Serializable()]
    public class Response
    {
        public bool Success { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string Message { get; set; }

        public Response() { }

        public Response(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        public Response(bool success, string message, string fileName, byte[] data)
        {
            this.Success = success;
            this.FileName = fileName;
            this.Data = data;
            this.Message = message;
        }
    }
}
