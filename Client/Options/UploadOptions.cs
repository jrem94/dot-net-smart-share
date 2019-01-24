using System;
using System.IO;
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
            var file = new FileInfo(options.FileName);
            Console.WriteLine($"Uploading {file.FullName}");
            Console.WriteLine($"Password: {options.Password}");
            return 0;
        }
    }
}