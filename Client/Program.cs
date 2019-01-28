using System;
using System.Collections.Generic;
using Client.Verbs;
using CommandLine;
using static Client.Verbs.DownloadOptions;
using static Client.Verbs.UploadOptions;

namespace Client
{
    public class Program
    {
        public static void Main()
        {
            var runCount = 1;
            
            // run a bunch of different commandline args through a simulated Main entry point
            foreach (var args in new List<string[]>
            {
                //experiment with different command line values here
                //new[] {"download", "test.txt", "password"}, 
                //new[] {"-h"},
                //new[] {"--version"},
                //new[] {"upload"},
                //new[] {"upload", "program.cs"},
                new[] {"upload", "testFile.txt", "password123"},
                //new[] {"download"},
                //new[] {"download", "program.cs"},
                //new[] {"download", "program.cs", "p@ssw0rd"},
                //new[] {"download", "program.cs", "password123"}
            })
            {
                Console.WriteLine("*** RUN #{0}, args: {1}", runCount++, String.Join(" ", args));
                __YourMain(args);
            }
        }

        public static void __YourMain(string[] args)
        {
            Parser.Default.ParseArguments<DownloadOptions, UploadOptions>(args)
                .MapResult(
                    (DownloadOptions opts) => ExecuteDownloadAndReturnExitCode(opts),
                    (UploadOptions opts) => ExecuteUploadAndReturnExitCode(opts),
                    errs => 1);
        }
    }
}