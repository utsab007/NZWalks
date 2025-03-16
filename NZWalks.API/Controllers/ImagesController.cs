using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadReqDto imageUploadReqDto)
        {
            ValidateFileUpload(imageUploadReqDto);

            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    File = imageUploadReqDto.File,
                    FileExtension = Path.GetExtension(imageUploadReqDto.File.FileName),
                    FileSizeInBytes = imageUploadReqDto.File.Length,
                    FileName = imageUploadReqDto.FileName,
                    FileDescription = imageUploadReqDto.FileDescription,
                };

                // user repository to save image to database
                var savedImage = await _imageRepository.AddImageAsync(image);

                return Ok(savedImage);

            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadReqDto imageUploadReqDto)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadReqDto.File.FileName)))
            {
                ModelState.AddModelError("File", "Invalid file extension. Only .jpg, .jpeg, .png are allowed.");
            }

            if (imageUploadReqDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size exceeds 10MB.");
            }
        }
    }

}
