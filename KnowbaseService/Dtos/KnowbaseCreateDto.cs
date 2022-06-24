﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KnowbaseService.Dtos
{
    public class KnowbaseCreateDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Owner { get; set; }
    }
}
