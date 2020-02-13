using System;
using System.Data;
using cduff.EmailProcessor.Data;
using cduff.EmailProcessor.Logic;
using cduff.EmailProcessor.Logic.TestEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cduff.EmailProcessor.Tests
{
    [TestClass]
    public class DefaultEmailRepoTests
    {
        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void GetEmailsTest()
        {
            try
            {
                string status = ConstantValues.Unprocessed;
                IEmailRepo emailRepo = new DefaultEmailRepo();

                DataAccessBase dataAccessBase = new DataAccessBase(TestSettings.DefaultConnectionString);
                DataTable expectedEmailTable = dataAccessBase.FillDataTable("[Config].[spu_Select_Email_By_Status]", new object[] { status });
                expectedEmailTable.Namespace = "Config";
                expectedEmailTable.TableName = "Email";

                DataTable actualEmailTable = emailRepo.GetEmails(status);

                foreach (DataRow expectedRow in expectedEmailTable.Rows)
                {
                    foreach (DataRow actualRow in actualEmailTable.Rows)
                    {
                        if (Convert.ToString(expectedRow["EmailId"]) != Convert.ToString(actualRow["EmailId"]))
                        {
                            continue;
                        }

                        foreach (DataColumn tableColumn in actualRow.Table.Columns)
                        {
                            if (Convert.ToString(expectedRow[tableColumn.ColumnName]) != Convert.ToString(actualRow[tableColumn.ColumnName]))
                            {
                                Assert.Fail();
                            }
                        }

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
