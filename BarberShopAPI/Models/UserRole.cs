﻿using System;
using System.Collections.Generic;

namespace BarberShopAPI.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            Users = new HashSet<User>();
        }

        public int UserRoleId { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
