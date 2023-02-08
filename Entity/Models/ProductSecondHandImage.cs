using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class ProductSecondHandImage
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int? RequestSellSecondHandId { get; set; }

        public virtual RequestSellSecondHand RequestSellSecondHand { get; set; }
    }
}
