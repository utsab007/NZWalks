using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContext;
        private readonly NZWalksDbContext _dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContext,
            NZWalksDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContext = httpContext;
            _dbContext = dbContext;
        }
        public async Task<Image> AddImageAsync(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", 
                $"{image.FileName}{image.FileExtension}");

            // upload image to localpath
            using var fileSteam = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(fileSteam);

            // https://localhost:5001/Images/imagename.jpg

            var urlPath = $"{_httpContext.HttpContext?.Request.Scheme}://{_httpContext.HttpContext?.Request.Host}" +
                $"{_httpContext.HttpContext?.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlPath;

            // save image to database
            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;
        }
    }
}
