using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entity.Models
{
    public partial class Size
    {
        public int Id { get; set; }
        public string SizeName { get; set; }
        public int? Price { get; set; }
        public int? ProductId { get; set; }
        public bool? IsActive { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Product Product { get; set; }
    }
}
