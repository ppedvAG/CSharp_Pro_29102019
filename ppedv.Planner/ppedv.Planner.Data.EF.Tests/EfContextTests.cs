using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.Planner.Model;
using System;

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

        [TestMethod]
        public void EfContext_can_CRUD_Mitarbeiter()
        {
            var m = new Mitarbeiter() { Name = $"Fred_{Guid.NewGuid()}", PersonalNummer = "P001" };

            //CREATE
            using (var con = new EfContext())
            {
                con.Mitarbeiter.Add(m);
                con.SaveChanges();
            }

            //READ
            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                Assert.IsNotNull(loaded);
                Assert.AreEqual(m.Name, loaded.Name);

                //UPDATE
                loaded.PersonalNummer = "X001";
                con.SaveChanges();
            }

            //check UPDATE
            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                Assert.AreEqual("X001", loaded.PersonalNummer);

                //DELETE
                con.Mitarbeiter.Remove(loaded);
                con.SaveChanges();
            }

            //check DELETE
            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                Assert.IsNull(loaded);
            }
        }
    }
}