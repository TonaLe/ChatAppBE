using api.DTO;
using api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IRelationshipRepository
    {
        void sendRequest(Relationship relationship);

        void rejectRequest(Relationship relationship);
        void acceptRequest(Relationship relationship);
        Task<RelationshipDto> getSingleRequest(int receiverId, int senderId, int status);

        Task<IEnumerable<RelationshipDto>> getAllRequest(int receiverId, int status);
    }
}
