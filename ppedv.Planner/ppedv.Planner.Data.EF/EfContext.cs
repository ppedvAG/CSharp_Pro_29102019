using ppedv.Planner.Model;
using System;
using System.Data.Entity;

namespace ppedv.Planner.Data.EF
{
    public class EfContext : DbContext
    {
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<MitarbeiterArt> MitarbeiterArten { get; set; }
        public DbSet<Urlaub> Urlaube { get; set; }

        public EfContext(string conString) : base(conString)
        { }

        public EfContext() : this("Server=.\\SQLEXPRESS;Database=PlannerDb_dev;Trusted_Connection=true;")
        { }
    }
}
