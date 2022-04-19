using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.Dtos;
using WebUI.Helpers;
using WebUI.Models;
using WebUI.Models.CatalogVM;
using WebUI.Services.Interfaces;

namespace WebUI.Services
{
    public class CatalogManager : ICatalogService
    {

        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogManager(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);
            if (resultPhotoService!=null)
            {
                courseCreateInput.Picture = resultPhotoService.Url;
            }
            var response = await _httpClient.PostAsJsonAsync("courses",courseCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadFromJsonAsync<ResponseDto<List<CategoryViewModel>>>();
            return responseData.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData=await response.Content.ReadFromJsonAsync<ResponseDto<List<CourseViewModel>>>();
            responseData.Data.ForEach(x => {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseData.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/getAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadFromJsonAsync<ResponseDto<List<CourseViewModel>>>();
            responseData.Data.ForEach(x=> {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseData.Data;
        }

        //  [controller]/GetAllByUserId/{userId}

        public async Task<CourseViewModel> GetByIdAsync(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            
            var responseData = await response.Content.ReadFromJsonAsync<ResponseDto<CourseViewModel>>();
            responseData.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(responseData.Data.Picture);
            return responseData.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);
            if (resultPhotoService != null)
            {
                await _photoStockService.DeletePhoto(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhotoService.Url;
            }
            var response = await _httpClient.PutAsJsonAsync("courses", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
