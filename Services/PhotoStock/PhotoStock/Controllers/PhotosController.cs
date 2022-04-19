using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.Dtos;
using Shared.ControllerBases;
using Shared.Dtos;

namespace PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> SavePhoto(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = photo.FileName;

                var photoDto = new PhotoForAddDto() { Url = returnPath };
                return CreateActionTResultInstance(ResponseDto<PhotoForAddDto>.Success(photoDto, 200));
            }
            return CreateActionTResultInstance(ResponseDto<PhotoForAddDto>.Fail("photo is empty", 400));
        }

        public IActionResult Delete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionTResultInstance(ResponseDto<NoContent>.Fail("photo Not Found", 404));
            }
            System.IO.File.Delete(path);
            return CreateActionTResultInstance(ResponseDto<NoContent>.Success(204));
        }
    }
}
