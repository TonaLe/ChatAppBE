using System;
using Microsoft.AspNetCore.Identity;

namespace api.Entities
{
    public class AppUsersRole : IdentityUserRole<int>
    {
        public AppUsers Users { get; set; }
        public AppRole Role { get; set; }
    }
}
