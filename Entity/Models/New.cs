using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class New
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Avatar { get; set; }
        public string Context { get; set; }
        public bool? IsActive { get; set; }
    }
}
