using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MessageManagement.Interfaces.Entities
{
    /// <summary>
    /// Message User
    /// </summary>
    public class MessageUser
    {
        public int Id { get; set; }

        [Required]
        public int MessageId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
