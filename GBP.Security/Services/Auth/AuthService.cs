using GBP.Core.Interfaces.Services;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Auth
{
    public class AuthService : IAuthService
    {
        public Task<User> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
