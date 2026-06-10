using GBP.Core.DTOs.Response;
using GBP.Core.Interfaces.Repositories;
using GBP.Core.Interfaces.Services.Auth;
using GBP.Core.Interfaces.Services.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Data
{
    public class UserService(
        IUserRepository _userRepository,
        IPasswordService _passwordService) : IUserService
    {
        public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponseDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role,
                Status = u.Status
            });
        }
    }
}
