using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.ShoeCheck
{
    public class CreateShoeCheckDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? ShoeName { get; set; }
        public int? CustomerId { get; set; }
    }
}
