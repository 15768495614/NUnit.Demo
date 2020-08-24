using NUnit.Framework;
using System.Transactions;

namespace NUnit.Test
{
    [TestFixture]
    public class DatabaseFixture
    {
        private TransactionScope _scope;

        [SetUp]
        public void Init()
        {
            _scope = new TransactionScope(TransactionScopeOption.Required);
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }
    }
}
