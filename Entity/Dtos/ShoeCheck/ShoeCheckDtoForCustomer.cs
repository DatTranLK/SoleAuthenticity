using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.ShoeCheck
{
    public class ShoeCheckDtoForCustomer
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ShoeName { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public DateTime? DateCompletedChecking { get; set; }
        public string StatusChecking { get; set; }
        public bool? IsAuthentic { get; set; }
        public int? CustomerId { get; set; }
    }
}
