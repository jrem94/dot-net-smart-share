using System;
using Client.Api;
using Client._Api;
using CommandLine;

namespace Client.Verbs
{
    [Verb("download", HelpText = "Downloads a file provided the correct password is given")]
    public class DownloadOptions
    {
        [Value(0, MetaName = "filename", HelpText = "Unique name of file to be downloaded", Required = true)]
        public string FileName { get; set; }
        
        [Value(1, MetaName = "password", HelpText = "Password used to access file", Required = true)]
        public string Password { get; set; }

        public static int ExecuteDownloadAndReturnExitCode(DownloadOptions options)
        {
            var downloadObject = Download(options);
            _Api.Api.ExecuteApiProcess(downloadObject);

            return 0;
        }

        public static SerializableOption Download(DownloadOptions options)
        {
            SerializableOption downloadObject = new SerializableOption(options.FileName, options.Password, "download");
            return downloadObject;
        }
    }
}