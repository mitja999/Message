using MessageManagement.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Interfaces.Models
{
    public class MessageExtended:Message
    {
        public List<int> UserIds { get; set; }
    }
}
