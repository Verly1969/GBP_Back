using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GBP.Core.Mapper
{
    public static class UserMapper
    {
        public static UserResponseDto ToUserResponseDto(this User user)
        {
            return new UserResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Status = user.Status
            };
        }

        public static IEnumerable<UserResponseDto> ToUserListResponseDtos(this IEnumerable<User> users) =>
            users.Select(user => user.ToUserResponseDto());
    }
}
