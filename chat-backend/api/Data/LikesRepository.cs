using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.Entities;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;

        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            //IQueryable<AppUsers> users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            IQueryable<AppUsers> users;
            var likes = _context.Likes.AsQueryable();

            //if (predicate == "liked")
            //{
            //    likes = likes.Where(l => l.SourceUserId == userId);
            //    users = likes.Select(l => l.LikedUser);
            //}

            if (predicate == "likedBy")
            {
                likes = likes.Where(l => l.LikedUserId == userId);
                users = likes.Select(l => l.SourceUser);
            }
            else
            {
                likes = likes.Where(l => l.SourceUserId == userId);
                users = likes.Select(l => l.LikedUser);
            }

            return await users.Select(user => new LikeDto
            {
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.GetAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                City = user.City,
                Id = user.Id
            }).ToListAsync();
        }

        public async Task<AppUsers> GetUserWithLikes(int userId)
        {
            return await _context.Users.Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        //public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        //{
        //    return await _context.Likes.FindAsync(sourceUserId, likedUserId);
        //}
    }
}
