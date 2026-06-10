using GBP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.DTOs.Response
{
    public class UserResponseDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public Role Role { get; set; }
        public Status Status { get; set; }
    }
}