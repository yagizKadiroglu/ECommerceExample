using Application.Services.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
       
        IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UploadImage([FromForm] IFormFileCollection files)
        {
            await _fileService.UploadAsync("resource\\product-images", files);

            return Ok();
        }
    }
}
