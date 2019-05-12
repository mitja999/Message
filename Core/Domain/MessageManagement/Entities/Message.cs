using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MessageManagement.Interfaces.Entities
{
    public class Message
    {
        public Message()
        {
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Template { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int State { get; set; }

    }
}
