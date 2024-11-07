﻿using OrderManagement.Core.Enums;

namespace OrderManagement.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Role { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
