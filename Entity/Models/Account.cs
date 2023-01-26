using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entity.Models
{
    public partial class Account
    {
        public Account()
        {
            OrderCustomers = new HashSet<Order>();
            OrderStaffs = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            ShoeCheckCustomers = new HashSet<ShoeCheck>();
            ShoeCheckStaffs = new HashSet<ShoeCheck>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsActive { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Order> OrderCustomers { get; set; }
        public virtual ICollection<Order> OrderStaffs { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ShoeCheck> ShoeCheckCustomers { get; set; }
        public virtual ICollection<ShoeCheck> ShoeCheckStaffs { get; set; }
    }
}
