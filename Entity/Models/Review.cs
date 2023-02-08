using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entity.Models
{
    public partial class Review
    {
        public Review()
        {
            Comments = new HashSet<Comment>();
        }

        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }
        public int? StaffId { get; set; }
        public string Description { get; set; }
        public string Elements { get; set; }
        public bool? IsActive { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Product Product { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Account Staff { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
