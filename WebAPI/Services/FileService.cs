using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> UploadAsync(
            IFormFile file,
            string folder)
        {
            if (file == null || file.Length == 0)
                throw new Exception("File is empty.");

            string extension = Path.GetExtension(file.FileName);

            string fileName =
                Guid.NewGuid().ToString() + extension;

            string uploadFolder =
                Path.Combine(
                    environment.WebRootPath,
                    "images",
                    folder);

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string filePath =
                Path.Combine(uploadFolder, fileName);

            using var stream =
                new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            return fileName;
        }

        public void Delete(
            string fileName,
            string folder)
        {
            string path =
                Path.Combine(
                    environment.WebRootPath,
                    "images",
                    folder,
                    fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}