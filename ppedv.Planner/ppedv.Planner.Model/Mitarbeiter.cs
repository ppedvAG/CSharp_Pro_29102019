using System.Collections.Generic;

namespace ppedv.Planner.Model
{
    public class Mitarbeiter : Entity
    {
        public string PersonalNummer { get; set; }
        public string Name { get; set; }
        public virtual MitarbeiterArt Art { get; set; }
        public virtual ICollection<Urlaub> Urlaube { get; set; } = new HashSet<Urlaub>();
    }
}