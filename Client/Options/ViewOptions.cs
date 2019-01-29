using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Verbs
{
    [Verb("view", HelpText = "Views a file's expiration and remaining downloads")]
    public class ViewOptions
    {
        [Value(0, MetaName = "filename", HelpText = "The file to be viewed", Required = true)]
        public string FileName { get; set; }

        [Value(1, MetaName = "password", HelpText = "Password for the file", Required = true)]
        public string Password { get; set; } 

        public static int ExecuteViewAndReturnExitCode(ViewOptions options)
        {
            Api.Api.View(options.FileName, options.Password);
            return 0;
        }
    }
}
