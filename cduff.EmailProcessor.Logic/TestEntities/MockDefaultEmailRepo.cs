using System;
using System.Data;
using cduff.EmailProcessor.Data;

namespace cduff.EmailProcessor.Logic.TestEntities
{
    public class MockDefaultEmailRepo : IEmailRepo
    {
        public DataTable GetEmails(string status)
        {
            DataTable emailTable = new DataTable
            {
                Namespace = "Config",
                TableName = "Email",
                Columns = 
                {
                    new DataColumn("EmailId"),
                    new DataColumn("Recipients"),
                    new DataColumn("CopyRecipients"),
                    new DataColumn("BlindCopyRecipients"),
                    new DataColumn("FromAddress"),
                    new DataColumn("ReplyTo"),
                    new DataColumn("SubjectText"),
                    new DataColumn("BodyText"),
                    new DataColumn("BodyFormat"),
                    new DataColumn("Importance"),
                    new DataColumn("Sensitivity"),
                    new DataColumn("FileAttachments"),
                    new DataColumn("Status"),
                    new DataColumn("TimeStamp")
                }
            };

            DataRow emailRow = emailTable.NewRow();
            emailRow["EmailId"] = TestSettings.EmailId;
            emailRow["Recipients"] = TestSettings.Recipients;
            emailRow["CopyRecipients"] = TestSettings.CopyRecipients;
            emailRow["BlindCopyRecipients"] = TestSettings.BlindCopyRecipients;
            emailRow["FromAddress"] = TestSettings.FromAddress;
            emailRow["ReplyTo"] = TestSettings.ReplyTo;
            emailRow["SubjectText"] = TestSettings.SubjectText;
            emailRow["BodyText"] = TestSettings.BodyText;
            emailRow["BodyFormat"] = TestSettings.BodyFormat;
            emailRow["Importance"] = TestSettings.Importance;
            emailRow["Sensitivity"] = TestSettings.Sensitivity;
            emailRow["FileAttachments"] = TestSettings.FileAttachments;
            emailRow["Status"] = TestSettings.Status;
            emailRow["TimeStamp"] = TestSettings.TimeStamp;

            emailTable.Rows.Add(emailRow);

            return emailTable;
        }

        public bool UpdateEmails(DataTable emailDataTable)
        {
            return true;
        }

        public bool DeleteEmails()
        {
            throw new NotImplementedException();
        }
    }
}
