using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.RequestSellSecondHand
{
    public class RequestSellSecondHandDto
    {
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
    }
}
