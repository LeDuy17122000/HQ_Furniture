using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileService fileService;

        public FileController(FileService fileService)
        {
            this.fileService = fileService;
        }

        // Upload Product Image
        [HttpPost("Product")]
        public async Task<IActionResult> UploadProduct(IFormFile file)
        {
            var fileName = await fileService.UploadAsync(file, "products");

            return Ok(new
            {
                FileName = fileName,
                Url = $"{Request.Scheme}://{Request.Host}/images/products/{fileName}"
            });
        }

        // Upload Category Image
        [HttpPost("Category")]
        public async Task<IActionResult> UploadCategory(IFormFile file)
        {
            var fileName = await fileService.UploadAsync(file, "categories");

            return Ok(new
            {
                FileName = fileName,
                Url = $"{Request.Scheme}://{Request.Host}/images/categories/{fileName}"
            });
        }

        // Upload User Avatar
        [HttpPost("Avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            var fileName = await fileService.UploadAsync(file, "users");

            return Ok(new
            {
                FileName = fileName,
                Url = $"{Request.Scheme}://{Request.Host}/images/users/{fileName}"
            });
        }

        // Delete File
        [HttpDelete]
        public IActionResult Delete(string fileName, string folder)
        {
            fileService.Delete(fileName, folder);

            return Ok("Deleted successfully.");
        }
    }
}