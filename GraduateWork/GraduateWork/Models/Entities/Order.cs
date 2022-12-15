using System;
using System.Collections.Generic;

namespace GraduateWork.Models.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string OrderStatus { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
