using System;
using System.Collections.Generic;
using System.Text;

namespace MessageManagement.Interfaces.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

    }
}
