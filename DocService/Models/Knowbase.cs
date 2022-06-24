using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Models
{
    public class Knowbase
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string owner { get; set; }

        [Required]
        public string img { get; set; }

        public ICollection<Doc> Docs { get; set; } = new List<Doc>();
    }
}
