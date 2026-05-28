using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Tools
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
