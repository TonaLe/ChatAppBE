using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.Entities;
using api.Helpers;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        
        public async Task<AppUsers> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUsers> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<PageList<MemberDto>> GetUsersAsync(UserParams userParams)
        {
            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge);
            var query = _context.Users.Include(p => p.Photos).AsSingleQuery()
                       .Where(u => u.UserName != userParams.CurrentUsername
                                                && u.Gender == userParams.Gender
                                                && u.DateOfBirth >= minDob
                                                && u.DateOfBirth <= maxDob)
                       .Select(user => new MemberDto
                       {
                           Id = user.Id,
                           Username = user.UserName,
                           PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                           Age = user.GetAge(),
                           KnownAs = user.KnownAs,
                           Created = user.Created,
                           LastActive = user.LastActive,
                           Gender = user.Gender,
                           Introduction = user.Introduction,
                           LookingFor = user.LookingFor,
                           Interests = user.Interests,
                           City = user.City,
                           Country = user.Country,
                           Photos = user.Photos.Select(photo => new PhotoDto
                           {
                               Id = photo.Id,
                               Url = photo.Url,
                               IsMain = photo.IsMain
                           }).ToList()
                       });
            //.ToListAsync();
            //.OrderBy(x => x.Id).AsNoTracking();
            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u =>u.Created),
                _ => query.OrderByDescending(u =>u.LastActive)
            };

            return await PageList<MemberDto>.CreateAsync(query, userParams.pageNumber, userParams.pageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUsers user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users.Where(x => x.UserName == username)
                .Select(user => new MemberDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                    Age = user.GetAge(),
                    KnownAs = user.KnownAs,
                    Created = user.Created,
                    LastActive = user.LastActive,
                    Gender = user.Gender,
                    Introduction = user.Introduction,
                    LookingFor = user.LookingFor,
                    Interests = user.Interests,
                    City = user.City,
                    Country = user.Country,
                    Photos = user.Photos.Select(photo => new PhotoDto
                    {
                        Id = photo.Id,
                        Url = photo.Url,
                        IsMain = photo.IsMain
                    }).ToList()
                }).SingleOrDefaultAsync();
                
        }

    }
}
