using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebUI.Models.PhotoStockVM;
using WebUI.Services.Interfaces;
using System.Net.Http.Json;
using Shared.Dtos;

namespace WebUI.Services
{
    public class PhotoStockManager : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string photoUrl)
        {
            var response = await _httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoStockViewModel> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
            {
                return null;
            }
            var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";
            using var memoryStream = new MemoryStream();

            await photo.CopyToAsync(memoryStream);
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new ByteArrayContent(memoryStream.ToArray()),"photo",randomFileName);
            var response = await _httpClient.PostAsync("photos",multipartContent);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess= await response.Content.ReadFromJsonAsync<ResponseDto<PhotoStockViewModel>>();
            return responseSuccess.Data;
        }
    }
}
