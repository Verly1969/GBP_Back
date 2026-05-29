using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Auth
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
