using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Incubators.Models.Repositories
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Incubator")]
        public int IncubatorId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public virtual Incubator Incubator { get; set; }
    }
}