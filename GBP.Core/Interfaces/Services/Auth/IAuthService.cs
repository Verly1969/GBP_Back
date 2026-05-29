using GBP.Core.DTOs.Response;
using GBP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(string email, string password);
        Task<LoginResponseDto> VerifyTwofactorAsync(string email, string code);
    }
}
