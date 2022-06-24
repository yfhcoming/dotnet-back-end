using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Dtos
{
    public class DocCreateDto
    {

        [Required]
        public string title { get; set; }

        [Required]
        public string content { get; set; }
    }
}
