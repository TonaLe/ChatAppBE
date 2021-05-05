using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTO;
using api.Entities;

namespace api.Interfaces
{
    public interface ILikesRepository
    {
        Task<AppUsers> GetUserWithLikes(int userId);

        Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);

        //Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
    }
}
