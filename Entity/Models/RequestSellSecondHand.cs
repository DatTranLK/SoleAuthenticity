using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entity.Models
{
    public partial class RequestSellSecondHand
    {
        public RequestSellSecondHand()
        {
            ProductSecondHandImages = new HashSet<ProductSecondHandImage>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string Quality { get; set; }
        public bool? IsFullbox { get; set; }
        public decimal? PriceBuy { get; set; }
        public decimal? PriceSell { get; set; }
        public string Warranty { get; set; }
        public string Contact { get; set; }
        public string RequestStatus { get; set; }
        public bool? IsRejected { get; set; }
        public bool? IsActive { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Account User { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<ProductSecondHandImage> ProductSecondHandImages { get; set; }
    }
}
