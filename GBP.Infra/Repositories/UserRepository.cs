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
        /// <summary>
        /// Ajoute un nouvel utilisateur à la base de données.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Le nouvel utilisateur créé ou null en cas d'échec</returns>
        public async Task<User?> AddAsync(User user)
        {
            if (user is null) return null;

            await context.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Retourne un utilisateur en fonction de son adresse e-mail.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>L'utilisateur trouvé ou null s'il n'existe pas</returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            if (email is null) return null;
            
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Modifie les informations d'un utilisateur existant dans la base de données.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>L'utilisateur mis à jour ou null en cas d'échec</returns>
        public async Task<User?> UpdateAsync(User user)
        {
            if (user is null) return null;

            context.Users.Update(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}
