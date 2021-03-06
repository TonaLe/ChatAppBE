using System;
using System.Threading.Tasks;
using api.Entities;

namespace api.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUsers user);
    }
}
