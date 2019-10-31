using ppedv.Planner.Model;
using ppedv.Planner.Model.Contracts;
using System;

namespace ppedv.Planner.Logic
{
    public class Core
    {
        public IRepository Repository { get; }

        public Core(IRepository repo)
        {
            this.Repository = repo;
        }

        public Core() : this(new Data.EF.EfRepository())
        { }

        public int CountTage(Urlaub urlaub)
        {
            throw new NotImplementedException();
        }

        public Mitarbeiter GetMitarbeiterMitDenMeistenUrlaubstagen()
        { 
            throw new NotImplementedException();
        }

    }
}
