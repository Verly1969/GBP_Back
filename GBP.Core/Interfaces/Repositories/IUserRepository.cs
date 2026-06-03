using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);

        Task<User?> UpdateAsync(User user);
    }
}
