using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using cduff.EmailProcessor.Data;

namespace cduff.EmailProcessor.Logic
{
    public class EmailManager : IEmailManager
    {
        private readonly IEmailRepo emailRepo;
        private readonly IEmailService emailService;

        public EmailManager(IEmailService emailService, IEmailRepo emailRepo)
        {
            this.emailService = emailService;
            this.emailRepo = emailRepo;
        }

        public void ProcessEmails()
        {
            DataTable unprocessedEmails = GetEmails(ConstantValues.Unprocessed);

            if (unprocessedEmails.Rows.Count <= 0)
            {
                return;
            }

            if (!SendEmails(unprocessedEmails))
            {
                throw new Exception("Failed to send emails");
            }

            if (!CommitEmailChanges(unprocessedEmails))
            {
                throw new Exception("Failed to commit email changes");
            }
        }

        public DataTable GetEmails(string status)
        {
            return emailRepo.GetEmails(status);
        }

        public bool SendEmails(DataTable unsentEmails)
        {
            return unsentEmails.Rows.Cast<DataRow>().All(SendEmailToAllRecipients);
        }

        public bool CommitEmailChanges(DataTable modifiedEmails)
        {
            modifiedEmails.AcceptChanges();
            if (UpdateEmail(modifiedEmails, ConstantValues.Processed))
            {
                return true;
            }

            UpdateEmail(modifiedEmails, ConstantValues.Unprocessed);
            return false;
        }

        #region Private Methods

        private bool UpdateEmail(DataTable unprocessedEmails, string status)
        {
            bool updateSuccess = unprocessedEmails.Rows.Cast<DataRow>().All(emailRow => UpdateEmailStatus(emailRow, status));

            return updateSuccess && emailRepo.UpdateEmails(unprocessedEmails);
        }

        private bool SendEmailToAllRecipients(DataRow emailRow)
        {
            string recipients = SetupEmailAddresses(emailRow["Recipients"].ToString());

            return recipients.Split(';').All(recipient => SendIndividualEmail(recipient, emailRow));
        }

        private static string SetupEmailAddresses(string recipients)
        {
            string[] userNames = recipients.TrimEnd(';').Split(';');
            recipients = string.Empty;

            foreach (string userName in userNames)
            {
                if (!userName.Contains("@"))
                {
                    recipients += userName + ConstantValues.EmailDomain + ";";
                }
                else
                {
                    recipients += userName + ";";
                }
            }

            return recipients.TrimEnd(';');
        }

        private bool SendIndividualEmail(string recipient, DataRow emailRow)
        {
            return emailService.SendEmail(BuildMailMessage(recipient, emailRow));
        }

        private static MailMessage BuildMailMessage(string recipient, DataRow emailRow)
        {
            MailMessage message = new MailMessage(AppSettings.FromAddress, recipient);

            SetupCcAddress(message, emailRow);
            SetupBccAddress(message, emailRow);
            SetupMessageSubject(message, emailRow);

            message.Body = emailRow["BodyText"].ToString();
            message.IsBodyHtml = emailRow["BodyFormat"].ToString().ToLower() == "html";

            MailPriority priority;
            message.Priority = Enum.TryParse(emailRow["Importance"].ToString(), true, out priority) ? priority : MailPriority.Normal;

            return message;
        }

        private static bool UpdateEmailStatus(DataRow emailRow, string status)
        {
            emailRow["Status"] = status;

            return true;
        }

        private static void SetupCcAddress(MailMessage message, DataRow emailRow)
        {
            string ccAddresses = emailRow["CopyRecipients"].ToString().TrimEnd(';');
            ccAddresses += string.IsNullOrEmpty(ccAddresses) ? AppSettings.CcAddress : ";" + AppSettings.CcAddress;

            if (string.IsNullOrEmpty(ccAddresses))
            {
                return;
            }

            foreach (string ccAddress in SetupEmailAddresses(ccAddresses).Split(';'))
            {
                message.CC.Add(new MailAddress(ccAddress));
            }
        }

        private static void SetupBccAddress(MailMessage message, DataRow emailRow)
        {
            string bccAddresses = emailRow["BlindCopyRecipients"].ToString().TrimEnd(';');
            bccAddresses += string.IsNullOrEmpty(bccAddresses) ? AppSettings.BccAddress : ";" + AppSettings.BccAddress;

            if (string.IsNullOrEmpty(bccAddresses))
            {
                return;
            }

            foreach (string bccAddress in SetupEmailAddresses(bccAddresses).Split(';'))
            {
                message.Bcc.Add(new MailAddress(bccAddress));
            }
        }

        private static void SetupMessageSubject(MailMessage message, DataRow emailRow)
        {
            if (!string.IsNullOrEmpty(emailRow["SubjectText"].ToString()))
            {
                message.Subject = emailRow["SubjectText"].ToString();
            }

            // failsafe to prevent test frame emails from being sent.
            message.Subject = string.Concat(AppSettings.SubjectPreprend, message.Subject);
        }

        #endregion
    }
}
