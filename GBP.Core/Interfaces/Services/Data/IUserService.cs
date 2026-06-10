using GBP.Core.DTOs.Response;
using GBP.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Data
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetUsersAsync();
    }
}
