using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class ShoeCheck
    {
        public ShoeCheck()
        {
            ShoeCheckImages = new HashSet<ShoeCheckImage>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string ShoeName { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public DateTime? DateCompletedChecking { get; set; }
        public string StatusChecking { get; set; }
        public bool? IsAuthentic { get; set; }
        public int? CustomerId { get; set; }
        public int? StaffId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Account Customer { get; set; }
        public virtual Account Staff { get; set; }
        public virtual ICollection<ShoeCheckImage> ShoeCheckImages { get; set; }
    }
}
