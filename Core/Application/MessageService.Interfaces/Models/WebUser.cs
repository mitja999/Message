using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Interfaces.Models
{
    public class WebUser
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
