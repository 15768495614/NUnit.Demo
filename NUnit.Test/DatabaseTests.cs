using Dapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Test
{
    [TestFixture]
    public class DatabaseTests : DatabaseFixture
    {
        [Test]
        public void InsertTest()
        {
            using (IDbConnection dbConnection = new SqlConnection("server=.;Initial Catalog=CTest;Integrated Security=True"))
            {
                dbConnection.Open();
                using (var trans = dbConnection.BeginTransaction())
                {
                    var sql = "INSERT INTO [dbo].[A] ([Name]) VALUES ('1347asdfasdfa')";
                    var a = dbConnection.Execute(sql, transaction: trans);
                    Assert.That(a, Is.EqualTo(1));
                    trans.Commit();
                }
            }
        }
    }
}
