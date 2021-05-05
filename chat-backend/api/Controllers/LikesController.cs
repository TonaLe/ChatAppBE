using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using api.DTO;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Entities;

namespace api.Controllers
{
    public class LikesController :BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikesRepository _likesRepository;

        public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
        {
            _userRepository = userRepository;
            _likesRepository = likesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
        {
            var userId = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            var users = await _likesRepository.GetUserLikes(predicate, userId);

            return Ok(users);
        }

        //[HttpPost("{username}")]
        //public async Task<ActionResult> AddLike(string username)
        //{
        //    var sourceUserId = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
        //    var likedUser = await _userRepository.GetUserByUsernameAsync(username);
        //    var sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId);

        //    if (likedUser == null) return NotFound();

        //    if (sourceUser.UserName == username) return BadRequest("You cannot like yourself");

        //    var userLike = await _likesRepository.GetUserLike(sourceUserId, likedUser.Id);

        //    if (userLike != null) return BadRequest("You already like this user");

        //    userLike = new UserLike
        //    {
        //        SourceUserId = sourceUserId,
        //        LikedUserId = likedUser.Id
        //    };

        //    sourceUser.LikedUsers.Add(userLike);

        //    if (await _userRepository.SaveAllAsync()) return Ok();

        //    return BadRequest("Failed to like user");
        //}
    }
}
