using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTO;
using api.Entities;
using api.Helpers;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUsers user);

        Task<bool> SaveAllAsync();

        Task<PageList<MemberDto>> GetUsersAsync(UserParams userParams);

        Task<AppUsers> GetUserByIdAsync(int id);

        Task<AppUsers> GetUserByUsernameAsync(string username);

        Task<MemberDto> GetMemberAsync(string username);
    }
}
