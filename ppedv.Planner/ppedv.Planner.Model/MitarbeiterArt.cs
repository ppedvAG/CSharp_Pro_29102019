using System.Collections.Generic;

namespace ppedv.Planner.Model
{
    public class MitarbeiterArt : Entity
    {
        public string Bezeichnung { get; set; }
        public int Urlaubstage { get; set; }
        public virtual ICollection<Mitarbeiter> Mitarbeiter { get; set; } = new HashSet<Mitarbeiter>();
    }
}