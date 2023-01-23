using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductImages = new HashSet<ProductImage>();
            Sizes = new HashSet<Size>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? AmountInStore { get; set; }
        public int? Price { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? AmountSold { get; set; }
        public bool? IsActive { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? StoreId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual Store Store { get; set; }
        public virtual Review Review { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Size> Sizes { get; set; }
    }
}
