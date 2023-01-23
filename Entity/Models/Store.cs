﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Store
    {
        public Store()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? Rate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
