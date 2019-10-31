using Bogus;
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

        public void CreateDemodaten()
        {
            var ma1 = new MitarbeiterArt() { Bezeichnung = "Vollzeitheld", Urlaubstage = 30 };
            var ma2 = new MitarbeiterArt() { Bezeichnung = "Halbzeittrottel", Urlaubstage = 15 };
            var ma3 = new MitarbeiterArt() { Bezeichnung = "Freizeitrentner", Urlaubstage = 4 };

            Repository.Add(ma1);
            Repository.Add(ma2);
            Repository.Add(ma3);

            var faker = new Faker<Mitarbeiter>()
                            .RuleFor(x => x.Name, f => f.Name.FullName());

            var ran = new Random();

            for (int i = 0; i < 20; i++)
            {
                var m = faker.Generate();
                m.PersonalNummer = $"P{i:000}";
                m.Art = new[] { ma1, ma2, ma3 }[ran.Next(0, 3)];

                for (int u = 0; u < ran.Next(1, 4); u++)
                {
                    var ur = new Urlaub() { Von = DateTime.Now.AddDays(ran.Next(100, 200) * -1) };
                    ur.Bis = ur.Von.AddDays(ran.Next(3, 14));
                    ur.Status = UrlaubsStatus.Genehmigt;
                    m.Urlaube.Add(ur);
                }
                Repository.Add(m);
            }

            Repository.SaveChanges();
        }


        public Mitarbeiter GetMitarbeiterMitDenMeistenUrlaubstagen()
        {
            throw new NotImplementedException();
        }

    }
}
