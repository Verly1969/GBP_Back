using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string email, string password);
    }
}
