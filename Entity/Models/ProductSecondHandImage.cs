using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entity.Models
{
    public partial class ProductSecondHandImage
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int? RequestSellSecondHandId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual RequestSellSecondHand RequestSellSecondHand { get; set; }
    }
}
