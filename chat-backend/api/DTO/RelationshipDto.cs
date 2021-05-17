using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public class RelationshipDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Status { get; set; }
    }
}
