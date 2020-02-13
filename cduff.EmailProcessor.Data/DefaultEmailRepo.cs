using System;
using System.Data;

namespace cduff.EmailProcessor.Data
{
    public class DefaultEmailRepo : IEmailRepo
    {
        private readonly DataAccessBase dataAccessBase;

        public DefaultEmailRepo()
        {
            dataAccessBase = new DataAccessBase(AppSettings.DefaultConnectionString);
        }

        public DataTable GetEmails(string status)
        {
            DataTable emailTable =
                dataAccessBase.FillDataTable("[Config].[spu_Select_Email_By_Status]", new object[] {status});
            emailTable.Namespace = "Config";
            emailTable.TableName = "Email";

            return emailTable;
        }

        public bool UpdateEmails(DataTable emailDataTable)
        {
            return dataAccessBase.UpdateDataTable("[Config].[spu_Upsert_Email]", emailDataTable);
        }

        public bool DeleteEmails()
        {
            throw new NotImplementedException();
        }
    }
}
