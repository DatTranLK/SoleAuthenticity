using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? AmountInStore { get; set; }
        public int? Price { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? AmountSold { get; set; }
        public bool? IsActive { get; set; }
    }
}
