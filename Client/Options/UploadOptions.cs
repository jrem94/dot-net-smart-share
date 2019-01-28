using System;
using System.IO;
using Client.Api;
using Client.Utils;
using CommandLine;

namespace Client.Verbs
{
    [Verb("upload", HelpText = "Uploads a file")]
    public class UploadOptions
    {
        [Value(0, MetaName = "filename", HelpText = "The file to be uploaded", Required = true)]
        public string FileName { get; set; }

        [Value(1, MetaName = "password", HelpText = "Password for the file", Required = false)]
        public string Password { get; set; } = PasswordGenerator.Generate();
        
        public static int ExecuteUploadAndReturnExitCode(UploadOptions options)
        {
            var uploadObject = Upload(options);
            _Api.Api.ExecuteApiProcess(uploadObject);
            return 0;
        }

        public static SerializableOption Upload(UploadOptions options)
        {
            SerializableOption uploadObject = new SerializableOption(options.FileName, options.Password, "upload");
            return uploadObject;
        }
    }
}