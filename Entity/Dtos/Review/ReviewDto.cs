using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Review
{
    public class ReviewDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }
        public int? StaffId { get; set; }
        public string Description { get; set; }
        public string Elements { get; set; }

        public string AuthorName { get; set; }
    }
}
