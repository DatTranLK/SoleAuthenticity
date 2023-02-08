using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int? ReviewId { get; set; }
        public int? UserId { get; set; }
        public string Body { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string UserName { get; set; }
    }
}
