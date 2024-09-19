using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DriveSafe.Presentation.Publishing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("Document/{documentName}", Name = "GetDocument")]
        public async Task<IActionResult> GetDocument(string documentName)
        {
            var path = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot/files/docs",
            documentName);

            var document = System.IO.File.OpenRead(path);
            return await Task.FromResult<IActionResult>(File(document, "application/pdf"));
        }

        // GET: api/File/fileName
        [AllowAnonymous]
        [HttpGet("Image/{imageName}", Name = "GetImage")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            var path = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot/files/images",
            imageName);

            var image = System.IO.File.OpenRead(path);
            return await Task.FromResult<IActionResult>(File(image, "image/jpeg"));
        }

        // POST: api/File
        [HttpPost]
        [Route("UploadImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostFile(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided");
            var fileName = $"image-{DateTime.Now.Ticks}.{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/files/images",
                fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }
            return Ok(new { fileName });
        }

        [HttpPost]
        [Route("UploadDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostDocument(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided");
            var fileName = $"document-{DateTime.Now.Ticks}.{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/files/docs",
                fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }
            return Ok(new { fileName });
        }
    }
}
