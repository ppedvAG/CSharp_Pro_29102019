using ppedv.Planner.Model;
using ppedv.Planner.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ppedv.Planner.Data.EF
{
    public class EfRepository : IRepository
    {
        EfContext context = new EfContext();

        public void Add<T>(T entity) where T : Entity
        {
            //if (T is Mitarbeiter)
            //    context.Mitarbeiter.Add(entity as Mitarbeiter);
            context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return context.Set<T>().ToList();
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return context.Set<T>();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            var loaded = context.Set<T>().Find(entity.Id);
            if (loaded != null)
            {
                context.Entry(loaded).CurrentValues.SetValues(entity);
            }
        }
    }
}
