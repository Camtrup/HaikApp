using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Haik.Models
{
    public class TripDb
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string JsonParticipantUids { get; set; }
        public string OwnerId { get; set; }
        public string Duration { get; set; }
        public string Difficulty { get; set; }
        public string Date { get; set; }
        public string Equipment { get; set; }
    }
}
