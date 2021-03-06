using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Haik.ViewModels
{
    public class WalkViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Participants { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string OwnerId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Location { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Duration { get; set; }
        [DataType(DataType.Text)]
        public string Difficulty { get; set; }
        public string Date { get; set; }
        public string Equipment { get; set; }
    }
}
