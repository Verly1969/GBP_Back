using GBP.Core.Interfaces.Repositories;
using GBP.Core.Interfaces.Services.Auth;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Auth
{
    public class AuthService(IUserRepository userRepository) : IAuthService
    {
        public Task<User> LoginAsync(string email, string password)
        {
            var user = userRepository.GetByEmailAsync(email);

            if (user == null) 
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            return user;
        }
    }
}