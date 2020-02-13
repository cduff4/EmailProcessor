using System;
using System.Net;
using System.Net.Mail;
using cduff.EmailProcessor.Data;

namespace cduff.EmailProcessor.Logic
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(MailMessage email)
        {
            SmtpClient smtpClient = new SmtpClient(AppSettings.EmailServer);
            try
            {
                /* The credentials returned by DefaultNetworkCredentials represents the authentication 
                 * credentials for the current security context in which the application is running. */
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;

                smtpClient.Send(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
