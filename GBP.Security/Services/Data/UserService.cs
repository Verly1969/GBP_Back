using GBP.Core.DTOs.Response;
using GBP.Core.Interfaces.Repositories;
using GBP.Core.Interfaces.Services.Auth;
using GBP.Core.Interfaces.Services.Data;
using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Security.Services.Data
{
    public class UserService(
        IUserRepository _userRepository) : IUserService
    {
        /// <summary>
        /// Retrieves all users from the database and maps them to UserResponseDto objects.
        /// </summary>
        /// <returns>A collection of UserResponseDto objects.</returns>
        public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponseDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role.ToString(),
                Status = u.Status.ToString()
            });
        }

        /// <summary>
        /// Toggles the status of a user between Active and Suspended based on their email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>True if the user's status was successfully changed, false otherwise.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<bool> ChangeStatusAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email)
                ?? throw new KeyNotFoundException($"User with email '{email}' not found.");

            user.Status = user.Status == Status.Active ? Status.Suspended : Status.Active;
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
