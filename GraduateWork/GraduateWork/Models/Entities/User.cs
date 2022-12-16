using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduateWork.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Request> Requests { get; set; }

        [NotMapped]
        public string UserFullName
        {
            get => $"{LastName} {FirstName[0]}. {MiddleName[0]}.";
        }
    }
}
