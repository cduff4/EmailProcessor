using System.Data;

namespace cduff.EmailProcessor.Data
{
    public interface IEmailRepo
    {
        DataTable GetEmails(string status);

        bool UpdateEmails(DataTable emailDataTable);

        bool DeleteEmails();
    }
}