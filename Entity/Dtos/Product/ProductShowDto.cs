using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Product
{
    public class ProductShowDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string ImgPath { get; set; }
    }
}
