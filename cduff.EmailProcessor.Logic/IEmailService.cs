using System.Net.Mail;

namespace cduff.EmailProcessor.Logic
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends email using smtp client
        /// </summary>
        /// <param name="email">The email to be sent</param>
        /// <returns>Email successfully sent</returns>
        bool SendEmail(MailMessage email);
    }
}
