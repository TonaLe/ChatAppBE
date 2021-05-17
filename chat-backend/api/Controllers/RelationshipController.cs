using api.DTO;
using api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    public class RelationshipController : BaseApiController
    {
        private readonly IRelationshipRepository relationshipRepository;

        public RelationshipController(IRelationshipRepository relationshipRepository)
        {
            this.relationshipRepository = relationshipRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelationshipDto>>> GetAllFriendRequests(int senderId)
        {
            var relationships = relationshipRepository.getAllRequest(senderId, 0);
            return Ok(relationships);
        }
    }
}
