using System.Net.Mail;

namespace cduff.EmailProcessor.Logic.TestEntities
{
    public class MockEmailService : IEmailService
    {
        public bool SendEmail(MailMessage email)
        {
            return true;
        }
    }
}
