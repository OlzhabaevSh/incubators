using Incubators.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Incubators.Models
{
    public partial class ApplicationDbContext
    {
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Incubator> Incubators { get; set; }

        public virtual DbSet<IncubatorPeriod> IncubatorPeriods { get; set; }
        public virtual DbSet<IncubatorMeasure> IncubatorMeasures { get; set; }
        public virtual DbSet<Event> Events { get; set; }
    }
}