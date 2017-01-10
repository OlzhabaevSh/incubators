using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using Incubators.Models;
using Incubators.Models.Repositories;

namespace Incubators.OdataControllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using Incubators.Models.Repositories;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Incubator>("Incubators");
    builder.EntitySet<Company>("Companies"); 
    builder.EntitySet<IncubatorMeasure>("IncubatorMeasures"); 
    builder.EntitySet<IncubatorPeriod>("IncubatorPeriods"); 
    builder.EntitySet<ApplicationUser>("ApplicationUsers"); 
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class IncubatorsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Incubators
        [EnableQuery]
        public IQueryable<Incubator> GetIncubators()
        {
            return db.Incubators;
        }

        // GET: odata/Incubators(5)
        [EnableQuery]
        public SingleResult<Incubator> GetIncubator([FromODataUri] int key)
        {
            return SingleResult.Create(db.Incubators.Where(incubator => incubator.Id == key));
        }

        // PUT: odata/Incubators(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Incubator> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Incubator incubator = db.Incubators.Find(key);
            if (incubator == null)
            {
                return NotFound();
            }

            patch.Put(incubator);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncubatorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(incubator);
        }

        // POST: odata/Incubators
        public IHttpActionResult Post(Incubator incubator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Incubators.Add(incubator);
            db.SaveChanges();

            return Created(incubator);
        }

        // PATCH: odata/Incubators(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Incubator> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Incubator incubator = db.Incubators.Find(key);
            if (incubator == null)
            {
                return NotFound();
            }

            patch.Patch(incubator);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncubatorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(incubator);
        }

        // DELETE: odata/Incubators(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Incubator incubator = db.Incubators.Find(key);
            if (incubator == null)
            {
                return NotFound();
            }

            db.Incubators.Remove(incubator);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Incubators(5)/Company
        [EnableQuery]
        public SingleResult<Company> GetCompany([FromODataUri] int key)
        {
            return SingleResult.Create(db.Incubators.Where(m => m.Id == key).Select(m => m.Company));
        }

        // GET: odata/Incubators(5)/Mesures
        [EnableQuery]
        public IQueryable<IncubatorMeasure> GetMesures([FromODataUri] int key)
        {
            return db.Incubators.Where(m => m.Id == key).SelectMany(m => m.Mesures);
        }

        // GET: odata/Incubators(5)/Periods
        [EnableQuery]
        public IQueryable<IncubatorPeriod> GetPeriods([FromODataUri] int key)
        {
            return db.Incubators.Where(m => m.Id == key).SelectMany(m => m.Periods);
        }

        // GET: odata/Incubators(5)/User
        [EnableQuery]
        public SingleResult<ApplicationUser> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Incubators.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IncubatorExists(int key)
        {
            return db.Incubators.Count(e => e.Id == key) > 0;
        }
    }
}
