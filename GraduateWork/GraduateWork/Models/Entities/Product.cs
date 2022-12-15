using System;
using System.Collections.Generic;

namespace GraduateWork.Models.Entities
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Image { get; set; } = null!;
        public decimal Cost { get; set; }
        public int CategoryId { get; set; }

        public virtual ProductCategory Category { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
