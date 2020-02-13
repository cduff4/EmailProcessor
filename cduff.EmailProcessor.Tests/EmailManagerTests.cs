using System.Data;
using cduff.EmailProcessor.Logic;
using cduff.EmailProcessor.Logic.TestEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cduff.EmailProcessor.Tests
{
    [TestClass]
    public class EmailManagerTests
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetEmailsTest()
        {
            const int expectedResult = 1;
            IEmailManager emailManager = new EmailManager(new MockEmailService(), new MockDefaultEmailRepo());

            int actualResult = emailManager.GetEmails(ConstantValues.Unprocessed).Rows.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void ProcessEmailsTest()
        {
            IEmailManager emailManager = new EmailManager(new MockEmailService(), new MockDefaultEmailRepo());
            int expectedResult = emailManager.GetEmails(ConstantValues.Unprocessed).Rows.Count;

            emailManager.ProcessEmails();

            int actualResult = emailManager.GetEmails(ConstantValues.Processed).Rows.Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void SendEmailsTest()
        {
            IEmailManager emailManager = new EmailManager(new MockEmailService(), new MockDefaultEmailRepo());
            DataTable emails = emailManager.GetEmails(ConstantValues.Unprocessed);

            bool result = emailManager.SendEmails(emails);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CommitEmailChangesTest()
        {
            IEmailManager emailManager = new EmailManager(new MockEmailService(), new MockDefaultEmailRepo());

            DataTable emails = emailManager.GetEmails(ConstantValues.Unprocessed);

            bool result = emailManager.CommitEmailChanges(emails);

            Assert.AreEqual(result, true);
        }
    }
}
