﻿using System.ComponentModel.DataAnnotations;

namespace AirlineDAL.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
