using Image.Gallery.WebAPP.Data;
using Image.Gallery.WebAPP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Image.Gallery.WebAPP.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApiController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public ApiController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Image>>> GetImages()
        {
            return await _context.Images.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Image>> GetImage(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        
        [HttpPost]
        //[Authorize] // Requires authentication
        public async Task<ActionResult<Models.Image>> UploadImage([FromForm] ImageUpload model)
        {
            if (model.Image == null || model.Image.Length == 0)
            {
                return BadRequest("Invalid image file");
            }

            if (!IsValidImageFormat(model.Image.ContentType))
            {
                return BadRequest("Invalid image format");
            }

            var image = new Models.Image
            {
                Title = model.Title,
                Description = model.Description,
                FileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName),
                Content = await GetByteArrayFromFormFile(model.Image)
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Home");
        }

       
        [HttpPost("{id}")]
        //[Authorize] // Requires authentication
        public async Task<IActionResult> UpdateImage(int id, [FromForm] ImageUpdate model)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            image.Title = model.Title ?? image.Title;
            image.Description = model.Description ?? image.Description;

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", "Home");
        }

        // Delete an image
        [HttpPost("{id}")]
        //[Authorize] // Requires authentication
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }

        private async Task<byte[]> GetByteArrayFromFormFile(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private bool IsValidImageFormat(string contentType)
        {
            return contentType.StartsWith("image/");
        }
    }
}
