using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Shared.Dtos;

namespace Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryForCreateDto categoryForCreateDto);
        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);
    }
}
