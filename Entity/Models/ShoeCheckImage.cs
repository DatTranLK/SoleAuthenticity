using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class ShoeCheckImage
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int? ShoeCheckId { get; set; }

        public virtual ShoeCheck ShoeCheck { get; set; }
    }
}
