using cduff.EmailProcessor.Logic;
using cduff.EmailProcessor.Data;

namespace cduff.EmailProcessor
{
    internal class Program
    {
        private static void Main()
        {
            ProcessEmails();
        }

        private static void ProcessEmails()
        {
            EmailManager emailManager = new EmailManager(new EmailService(), new DefaultEmailRepo());

            emailManager.ProcessEmails();
        }
    }
}
