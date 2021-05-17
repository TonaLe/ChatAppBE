using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public class RelationshipDto
    {
        public RelationshipDto(int receiverId, int senderId, int status)
        {
            ReceiverId = receiverId;
            SenderId = senderId;
            Status = status;
        }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int Status { get; set; }
    }
}
