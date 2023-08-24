using System;
using System.Collections.Generic;

namespace BarberShopAPI.Models
{
    public partial class User
    {
        public User()
        {
            Cita = new HashSet<Citum>();
        }

        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public bool? Active { get; set; }
        public int UserRoleId { get; set; }

        //public string? DescripcionRol { get; set; } = null!;

        public virtual UserRole? UserRole { get; set; } = null!;


        public virtual ICollection<Citum> Cita { get; set; }
    }
}
