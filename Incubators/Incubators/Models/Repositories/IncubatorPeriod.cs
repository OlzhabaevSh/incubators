using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Incubators.Models.Repositories
{
    public class IncubatorPeriod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Incubator")]
        public int IncubatorId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public int Timespan { get; set; }
        public int Order { get; set; }

        public virtual Incubator Incubator { get; set; }
        public virtual ICollection<IncubatorMeasure> Mesures { get; set; }
    }
}