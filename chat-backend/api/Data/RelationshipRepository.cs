using api.DTO;
using api.Entities;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Data
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private readonly DataContext _context;

        public RelationshipRepository()
        {
        }

        public RelationshipRepository(DataContext context)
        {
            _context = context;
        }

        public void acceptRequest(Relationship relationship)
        {
            _context.Relationships.AddAsync(relationship);
        }

        public async Task<IEnumerable<RelationshipDto>> getAllRequest(int receiverId, int status)
        {
            return await _context.Relationships
                .Where(relationship => relationship.ReceiverId.Equals(receiverId)
                && relationship.Status.Equals(status))
                .Select(friendRequest => new RelationshipDto(friendRequest.ReceiverId,
                friendRequest.SenderId, friendRequest.Status))
                .ToListAsync();
        }

        public async Task<RelationshipDto> getSingleRequest(int receiverId, int senderId, int status)
        {
            return await _context.Relationships
                .Where(relationship => relationship.ReceiverId.Equals(receiverId) 
                && relationship.SenderId.Equals(senderId)
                && relationship.Status.Equals(status))
                .Select(friendRequest => new RelationshipDto(friendRequest.ReceiverId, 
                friendRequest.SenderId, friendRequest.Status))
                .SingleOrDefaultAsync();
        }

        public void rejectRequest(Relationship relationship)
        {
            _context.Relationships.Update(relationship);
        }

        public void sendRequest(Relationship relationship)
        {
            _context.Relationships.Update(relationship);
        }


    }
}
