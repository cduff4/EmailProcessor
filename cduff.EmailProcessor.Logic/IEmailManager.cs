using System.Data;

namespace cduff.EmailProcessor.Logic
{
    public interface IEmailManager
    {
        /// <summary>
        /// Gets unprocessed emails, sends the emails, updates the database to show emails are now processed.
        /// </summary>
        void ProcessEmails();

        /// <summary>
        /// Retrieves all unprocessed emails from Config.Email table of a particular status
        /// </summary>
        DataTable GetEmails(string status);

        /// <summary>
        /// Use SMTP mail to send unprocessed emails
        /// </summary>
        bool SendEmails(DataTable unsentEmails);

        /// <summary>
        /// Update sent status of emails in database
        /// </summary>
        bool CommitEmailChanges(DataTable modifiedEmails);
    }
}
