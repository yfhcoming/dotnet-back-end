using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Dtos
{
    public class DocReadDto
    {
        public int Id { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public int KnowbaseId { get; set; }
    }
}
