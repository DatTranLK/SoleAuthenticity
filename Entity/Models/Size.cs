using System;
using System.Collections.Generic;

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

        public virtual Product Product { get; set; }
    }
}
