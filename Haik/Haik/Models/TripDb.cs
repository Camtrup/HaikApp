using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haik.Models
{
    public class TripDb
    {
        public int ID { get; set; } 
        public string Description { get; set; }
        public string Participants { get; set; } 
        public string OwnerId { get; set; } 
    }
}
