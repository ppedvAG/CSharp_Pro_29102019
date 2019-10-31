using ppedv.Planner.Logic;
using ppedv.Planner.Model;
using System;

namespace ppedv.Planner.UI.DevConsole_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var core = new Core();

            foreach (var m in core.Repository.GetAll<Mitarbeiter>())
            {
                Console.WriteLine($"{m.Name} {m.PersonalNummer}");
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
