using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Incubators.Models.Repositories
{
    public class Incubator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime? StartedOn { get; set; } 
        public DateTime? FinishingOn { get; set; }
        public int Eggs { get; set; }
        
        [ForeignKey("Company")]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<IncubatorMeasure> Mesures { get; set; }
        public virtual ICollection<IncubatorPeriod> Periods { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}