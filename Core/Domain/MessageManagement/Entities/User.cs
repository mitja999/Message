﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MessageManagement.Interfaces.Entities
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool Active { get; set; }

    }
}
