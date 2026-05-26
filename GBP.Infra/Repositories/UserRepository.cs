using GBP.Core.Interfaces.Repositories;
using GBP.Domain.Entities;
using GBP.Infra.Database.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GBP.Infra.Repositories
{
    public class UserRepository(GbpDbContext context) : IUserRepository
    {
        public async Task<User?> AddAsync(User user)
        {
            if (user is null) return null;

            await context.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (email is null) return null;
            
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
