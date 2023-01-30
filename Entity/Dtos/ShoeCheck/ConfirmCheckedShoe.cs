using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.ShoeCheck
{
    public class ConfirmCheckedShoe
    {
        public DateTime? DateCompletedChecking { get; set; }
        public string StatusChecking { get; set; }
        public bool? IsAuthentic { get; set; }
        public int? StaffId { get; set; }
    }
}
