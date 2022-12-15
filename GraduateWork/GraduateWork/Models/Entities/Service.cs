using System;
using System.Collections.Generic;

namespace GraduateWork.Models.Entities
{
    public partial class Service
    {
        public Service()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
