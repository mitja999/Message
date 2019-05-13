using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Interfaces.Models
{
    public class MessageUsers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Template { get; set; }

        public int Type { get; set; }

        public int State { get; set; }
    }
}
