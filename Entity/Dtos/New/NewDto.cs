using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.New
{
    public class NewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Avatar { get; set; }
        public string Context { get; set; }
        public bool? IsActive { get; set; }
    }
}
