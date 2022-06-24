using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KnowbaseService.Models
{
    public class Knowbase
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Owner { get; set; }

        [Required]
        public string img { get; set; }

/*        [Required]
        public string Cost { get; set; }*/
    }
}
