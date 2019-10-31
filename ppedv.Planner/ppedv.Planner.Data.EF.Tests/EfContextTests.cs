using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ppedv.Planner.Data.EF.Tests
{
    [TestClass]
    public class EfContextTests
    {
        [TestMethod]
        public void EfContext_can_create_database()
        {
            var context = new EfContext();

            if (context.Database.Exists())
                context.Database.Delete();

            Assert.IsFalse(context.Database.Exists());
            context.Database.Create();
            Assert.IsTrue(context.Database.Exists());
        }
    }
}
