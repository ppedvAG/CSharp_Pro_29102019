using ppedv.Planner.Logic;
using ppedv.Planner.Model;
using System;
using System.Linq;

namespace ppedv.Planner.UI.DevConsole_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var core = new Core();

            //COUNT per SQL => gut
            if (core.Repository.Query<Mitarbeiter>().Count() == 0)
            {
                core.CreateDemodaten();
            }

            //lädt alles in den speicher und zähl => doof
            //if (core.Repository.GetAll<Mitarbeiter>().Count() == 0) { }

            foreach (var m in core.Repository.GetAll<Mitarbeiter>())
            {
                Console.WriteLine($"{m.Name} {m.PersonalNummer} [{m.Art.Bezeichnung} ({m.Art.Urlaubstage})]");
                foreach (var ur in m.Urlaube)
                {
                    Console.WriteLine($"\t{ur.Von:d}-{ur.Bis:d} {ur.Status}");
                }
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
