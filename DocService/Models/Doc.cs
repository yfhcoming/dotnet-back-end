using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Models
{
    public class Doc
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string content { get; set; }

        [Required]
        public int KnowbaseId { get; set; }


        /*        [Required]
                public string tag { get; set; }*/


        public Knowbase Knowbase { get; set; }
    }
}
