using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.ShoeCheckImage
{
    public class ShoeCheckImageDto
    {
        public int Id { get; set; }
        public string? ImgPath { get; set; }
        public int? ShoeCheckId { get; set; }
    }
}
