using System.Configuration;

namespace cduff.EmailProcessor.Data
{
    /// <summary>
    /// Wrapper class that exposes App Config settings as properties
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// The default connection string
        /// </summary>
        public static string DefaultConnectionString => ConfigurationManager.AppSettings["DbServer"];

        /// <summary>
        /// The From Email Address to send emails from
        /// </summary>
        public static string FromAddress => ConfigurationManager.AppSettings["FromAddress"];

        /// <summary>
        /// The Carbon Copy Email Address to send emails to
        /// </summary>
        public static string CcAddress => ConfigurationManager.AppSettings["CcAddress"];

        /// <summary>
        /// The Blind Copy Email Address to send emails to
        /// </summary>
        public static string BccAddress => ConfigurationManager.AppSettings["BccAddress"];

        /// <summary>
        /// Text to prepend subjects for test emails
        /// </summary>
        public static string SubjectPreprend => ConfigurationManager.AppSettings["SubjectPreprend"];

        /// <summary>
        /// The Email Server to connect to
        /// </summary>
        public static string EmailServer => ConfigurationManager.AppSettings["EmailServer"];
    }
}
