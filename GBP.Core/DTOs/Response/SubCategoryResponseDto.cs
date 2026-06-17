using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Core.DTOs.Response
{
    public class SubCategoryResponseDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public int CategoryId { get; init; }
        public string CategoryName { get; init; } = null!;
    }
}
