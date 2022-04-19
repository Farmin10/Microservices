using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Services.Catalog.Settings;
using Shared.Dtos;

namespace Services.Catalog.Services
{
    public class CategoryManager: ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryManager(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categories=await _categoryCollection.Find(category => true).ToListAsync();
            return  ResponseDto<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories),200);
        }


        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryForCreateDto categoryForCreateDto)
        {
            var newCategory = _mapper.Map<Category>(categoryForCreateDto);
             await _categoryCollection.InsertOneAsync(newCategory);
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(newCategory),200);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(category => category.Id==id).SingleOrDefaultAsync();
            if (category==null)
            {
                return ResponseDto<CategoryDto>.Fail("Category Not Found", 404);
            }
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
