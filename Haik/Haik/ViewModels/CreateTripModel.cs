using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haik.ViewModels
{
    public class CreateTripModel
    {
        public string description { get; set; }
        public string location { get; set; }
        public string difficulty { get; set; }
        public DateTime date { get; set; }
        public string[] equipment { get; set; }
        public float duration { get; set; }
        public string title { get; set; }
    }
}
