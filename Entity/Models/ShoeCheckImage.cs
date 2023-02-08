using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entity.Models
{
    public partial class ShoeCheckImage
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int? ShoeCheckId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ShoeCheck ShoeCheck { get; set; }
    }
}
