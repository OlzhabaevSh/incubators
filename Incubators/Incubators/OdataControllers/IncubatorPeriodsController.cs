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
    builder.EntitySet<IncubatorPeriod>("IncubatorPeriods");
    builder.EntitySet<Incubator>("Incubators"); 
    builder.EntitySet<IncubatorMeasure>("IncubatorMeasures"); 
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class IncubatorPeriodsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/IncubatorPeriods
        [EnableQuery]
        public IQueryable<IncubatorPeriod> GetIncubatorPeriods()
        {
            return db.IncubatorPeriods;
        }

        // GET: odata/IncubatorPeriods(5)
        [EnableQuery]
        public SingleResult<IncubatorPeriod> GetIncubatorPeriod([FromODataUri] int key)
        {
            return SingleResult.Create(db.IncubatorPeriods.Where(incubatorPeriod => incubatorPeriod.Id == key));
        }

        // PUT: odata/IncubatorPeriods(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<IncubatorPeriod> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IncubatorPeriod incubatorPeriod = db.IncubatorPeriods.Find(key);
            if (incubatorPeriod == null)
            {
                return NotFound();
            }

            patch.Put(incubatorPeriod);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncubatorPeriodExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(incubatorPeriod);
        }

        // POST: odata/IncubatorPeriods
        public IHttpActionResult Post(IncubatorPeriod incubatorPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IncubatorPeriods.Add(incubatorPeriod);
            db.SaveChanges();

            return Created(incubatorPeriod);
        }

        // PATCH: odata/IncubatorPeriods(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<IncubatorPeriod> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IncubatorPeriod incubatorPeriod = db.IncubatorPeriods.Find(key);
            if (incubatorPeriod == null)
            {
                return NotFound();
            }

            patch.Patch(incubatorPeriod);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncubatorPeriodExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(incubatorPeriod);
        }

        // DELETE: odata/IncubatorPeriods(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            IncubatorPeriod incubatorPeriod = db.IncubatorPeriods.Find(key);
            if (incubatorPeriod == null)
            {
                return NotFound();
            }

            db.IncubatorPeriods.Remove(incubatorPeriod);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/IncubatorPeriods(5)/Incubator
        [EnableQuery]
        public SingleResult<Incubator> GetIncubator([FromODataUri] int key)
        {
            return SingleResult.Create(db.IncubatorPeriods.Where(m => m.Id == key).Select(m => m.Incubator));
        }

        // GET: odata/IncubatorPeriods(5)/Mesures
        [EnableQuery]
        public IQueryable<IncubatorMeasure> GetMesures([FromODataUri] int key)
        {
            return db.IncubatorPeriods.Where(m => m.Id == key).SelectMany(m => m.Mesures);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IncubatorPeriodExists(int key)
        {
            return db.IncubatorPeriods.Count(e => e.Id == key) > 0;
        }
    }
}
