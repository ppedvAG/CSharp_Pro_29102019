using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ppedv.Planner.Logic;
using ppedv.Planner.Model;

namespace ppedv.Planner.UI.Web.Controllers
{
    public class MitarbeiterController : Controller
    {
        Core core = new Core();

        // GET: Mitarbeiter
        public ActionResult Index()
        {
            return View(core.Repository.GetAll<Mitarbeiter>());
        }

        // GET: Mitarbeiter/Details/5
        public ActionResult Details(int id)
        {

            return View(core.Repository.Query<Mitarbeiter>().FirstOrDefault(x => x.Id == id));
        }

        // GET: Mitarbeiter/Create
        public ActionResult Create()
        {
            return View(new Mitarbeiter() { Name = "Fred" });
        }

        // POST: Mitarbeiter/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mitarbeiter m)
        {
            try
            {
                core.Repository.Add(m);
                core.Repository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Mitarbeiter/Edit/5
        public ActionResult Edit(int id)
        {
            return View(core.Repository.Query<Mitarbeiter>().FirstOrDefault(x => x.Id == id));
        }

        // POST: Mitarbeiter/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Mitarbeiter m)
        {
            try
            {
                core.Repository.Update(m);
                core.Repository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Mitarbeiter/Delete/5
        public ActionResult Delete(int id)
        {
            return View(core.Repository.Query<Mitarbeiter>().FirstOrDefault(x => x.Id == id));

        }

        // POST: Mitarbeiter/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Mitarbeiter m)
        {
            try
            {
                var loaded = core.Repository.Query<Mitarbeiter>().FirstOrDefault(x => x.Id == id);
                if (loaded != null)
                {
                    foreach (var u in loaded.Urlaube.ToList())
                    {
                        core.Repository.Delete(u);
                    }
                    core.Repository.Delete(loaded);
                    core.Repository.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}