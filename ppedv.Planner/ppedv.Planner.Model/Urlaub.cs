using System;

namespace ppedv.Planner.Model
{
    public class Urlaub : Entity
    {
        public virtual Mitarbeiter Mitarbeiter { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        public UrlaubsStatus Status { get; set; }
    }

    public enum UrlaubsStatus
    {
        Geplant,
        Beantragt,
        Abgelehnt,
        Genehmigt
    }
}