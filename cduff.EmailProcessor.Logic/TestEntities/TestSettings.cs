using System.Configuration;

namespace cduff.EmailProcessor.Logic.TestEntities
{
    public class TestSettings
    {
        public static string DefaultConnectionString => ConfigurationManager.AppSettings["DbServer"];

        public static string EmailId => ConfigurationManager.AppSettings["EmailId"];

        public static string Recipients => ConfigurationManager.AppSettings["Recipients"];

        public static string CopyRecipients => ConfigurationManager.AppSettings["CopyRecipients"];

        public static string BlindCopyRecipients => ConfigurationManager.AppSettings["BlindCopyRecipients"];

        public static string FromAddress => ConfigurationManager.AppSettings["FromAddress"];

        public static string ReplyTo => ConfigurationManager.AppSettings["ReplyTo"];

        public static string SubjectText => ConfigurationManager.AppSettings["SubjectText"];

        public static string BodyText => ConfigurationManager.AppSettings["BodyText"];

        public static string BodyFormat => ConfigurationManager.AppSettings["BodyFormat"];

        public static string Importance => ConfigurationManager.AppSettings["Importance"];

        public static string Sensitivity => ConfigurationManager.AppSettings["Sensitivity"];

        public static string FileAttachments => ConfigurationManager.AppSettings["FileAttachments"];

        public static string Status => ConfigurationManager.AppSettings["Status"];

        public static string TimeStamp => ConfigurationManager.AppSettings["TimeStamp"];
    }
}
