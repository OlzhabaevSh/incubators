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
    builder.EntitySet<IncubatorMeasure>("IncubatorMeasures");
    builder.EntitySet<Incubator>("Incubators"); 
    builder.EntitySet<IncubatorPeriod>("IncubatorPeriods"); 
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class IncubatorMeasuresController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/IncubatorMeasures
        [EnableQuery]
        public IQueryable<IncubatorMeasure> GetIncubatorMeasures()
        {
            return db.IncubatorMeasures;
        }

        // GET: odata/IncubatorMeasures(5)
        [EnableQuery]
        public SingleResult<IncubatorMeasure> GetIncubatorMeasure([FromODataUri] int key)
        {
            return SingleResult.Create(db.IncubatorMeasures.Where(incubatorMeasure => incubatorMeasure.Id == key));
        }

        // PUT: odata/IncubatorMeasures(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<IncubatorMeasure> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IncubatorMeasure incubatorMeasure = db.IncubatorMeasures.Find(key);
            if (incubatorMeasure == null)
            {
                return NotFound();
            }

            patch.Put(incubatorMeasure);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncubatorMeasureExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(incubatorMeasure);
        }

        // POST: odata/IncubatorMeasures
        public IHttpActionResult Post(IncubatorMeasure incubatorMeasure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IncubatorMeasures.Add(incubatorMeasure);
            db.SaveChanges();

            return Created(incubatorMeasure);
        }

        // PATCH: odata/IncubatorMeasures(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<IncubatorMeasure> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IncubatorMeasure incubatorMeasure = db.IncubatorMeasures.Find(key);
            if (incubatorMeasure == null)
            {
                return NotFound();
            }

            patch.Patch(incubatorMeasure);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncubatorMeasureExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(incubatorMeasure);
        }

        // DELETE: odata/IncubatorMeasures(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            IncubatorMeasure incubatorMeasure = db.IncubatorMeasures.Find(key);
            if (incubatorMeasure == null)
            {
                return NotFound();
            }

            db.IncubatorMeasures.Remove(incubatorMeasure);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/IncubatorMeasures(5)/Incubator
        [EnableQuery]
        public SingleResult<Incubator> GetIncubator([FromODataUri] int key)
        {
            return SingleResult.Create(db.IncubatorMeasures.Where(m => m.Id == key).Select(m => m.Incubator));
        }

        // GET: odata/IncubatorMeasures(5)/Period
        [EnableQuery]
        public SingleResult<IncubatorPeriod> GetPeriod([FromODataUri] int key)
        {
            return SingleResult.Create(db.IncubatorMeasures.Where(m => m.Id == key).Select(m => m.Period));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IncubatorMeasureExists(int key)
        {
            return db.IncubatorMeasures.Count(e => e.Id == key) > 0;
        }
    }
}
