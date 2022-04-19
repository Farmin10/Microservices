using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Catalog.Dtos;
using Shared.Dtos;

namespace Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<ResponseDto<List<CourseDto>>> GetAllAsync();
        Task<ResponseDto<CourseDto>> GetByIdAsync(string id);
        Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<ResponseDto<CourseDto>> CreateAsync(CourseForCreateDto courseForCreateDto);
        Task<ResponseDto<NoContent>> UpdateAsync(CourseForUpdateDto courseForUpdateDto);
        Task<ResponseDto<NoContent>> DeleteAsync(string id);
    }
}
