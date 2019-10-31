using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ppedv.Planner.Logic;
using ppedv.Planner.Model;
using ppedv.Planner.UI.Web.Models;

namespace ppedv.Planner.UI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MitarbeiterAPIController : ControllerBase
    {
        Core core = new Core();

        // GET: api/MitarbeiterAPI
        [HttpGet]
        public IEnumerable<MitarbeiterAPI> Get()
        {
            foreach (var m in core.Repository.GetAll<Mitarbeiter>())
            {
                yield return new MitarbeiterAPI() { Id = m.Id, Name = m.Name, PNummer = m.PersonalNummer };
            }
        }

        // GET: api/MitarbeiterAPI/5
        [HttpGet("{id}", Name = "Get")]
        public MitarbeiterAPI Get(int id)
        {
            var m = core.Repository.Query<Mitarbeiter>().FirstOrDefault(x => x.Id == id);

            return new MitarbeiterAPI() { Id = m.Id, Name = m.Name, PNummer = m.PersonalNummer };
        }


        // POST: api/MitarbeiterAPI
        [HttpPost]
        public void Post([FromBody] MitarbeiterAPI m)
        {
            core.Repository.Add(new Mitarbeiter() { Name = m.Name, PersonalNummer = m.PNummer });
            core.Repository.SaveChanges();
        }

        // PUT: api/MitarbeiterAPI/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MitarbeiterAPI m)
        {
            var update = new Mitarbeiter() {Id =m.Id,  Name = m.Name, PersonalNummer = m.PNummer };
            core.Repository.Update(update);
            core.Repository.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var m = core.Repository.Query<Mitarbeiter>().FirstOrDefault(x => x.Id == id);
            core.Repository.Delete(m);
            core.Repository.SaveChanges();
        }
    }
}
