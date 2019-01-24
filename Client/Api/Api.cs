using System;
using System.Runtime.ConstrainedExecution;

namespace Client.Api
{
    public class Api
    {
        private const string HOST = "localhost";
        private const int PORT = 3000;

        private Api()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Send download request
        /// </summary>
        /// <param name="">TODO</param>
        /// <returns>true if request was successful and false if unsuccessful</returns>
        public static bool Download(bool test)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send upload request
        /// </summary>
        /// <param name="">TODO</param>
        /// <returns>true if request was successful and false if unsuccessful</returns>
        public static bool Upload()
        {
            throw new NotImplementedException();
        }
    }
}