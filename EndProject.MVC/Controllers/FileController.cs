using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace EndProject.MVC.Controllers
{
    public class FileController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7032/api");
        private readonly HttpClient _httpClient;

        public FileController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> GetFile(string folderName, string fileName)
        {
            var response = await _httpClient.GetAsync(baseAddress + "/File/" + $"{folderName}/{fileName}");

            if (response.IsSuccessStatusCode)
            {
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                var contentType = response.Content.Headers.ContentType.ToString();

                return File(fileBytes, contentType, fileName);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
