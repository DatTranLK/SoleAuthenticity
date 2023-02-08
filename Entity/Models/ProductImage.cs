using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entity.Models
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int? ProductId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Product Product { get; set; }
    }
}
