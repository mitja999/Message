using System;
using System.Collections.Generic;
using System.Text;

namespace MessageManagement.Interfaces.Entities
{
    public class MessageUser
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
    }
}
