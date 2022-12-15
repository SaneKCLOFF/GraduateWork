using System;
using System.Collections.Generic;

namespace GraduateWork.Models.Entities
{
    public partial class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public string RequestStatus { get; set; } = null!;

        public virtual Service Service { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
