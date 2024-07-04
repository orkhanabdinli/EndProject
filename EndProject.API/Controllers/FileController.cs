using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EndProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public FileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet("{folderName}/{fileName}")]
        public IActionResult GetFile(string folderName, string fileName)
        {
            var filePath = Path.Combine(_environment.WebRootPath, folderName, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var contentType = MimeTypes.GetMimeType(filePath);

            return File(fileBytes, contentType, fileName);
        }
    }
}
