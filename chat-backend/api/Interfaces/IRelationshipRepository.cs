using api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IRelationshipRepository
    {
        void sendRequest(int senderId, int receiverId, string status);

        void rejectRequest(int id);
        void acceptRequest(int id);
        Task<RelationshipDto> getSingleRequest(int receiverId);

        Task<IEnumerable<RelationshipDto>> getAllRequest(int receiverId);
    }
}
