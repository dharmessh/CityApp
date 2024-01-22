using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace City.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(int fileId)
        {
            var filePath = "Sukhoi.PNG";
            if(!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes,"image/png",Path.GetFileName(filePath));     //Content Type Should be Set as Per File Extension otherwise file will be corrupted --< "text/plain"  
        }                                                                   //but this will resolve our problem only for a specific file type

        [HttpGet("FileExtension/{FileId}")]
        public ActionResult GetFileByFileId(int FileId)
        {
            var filePath = "Sukhoi.PNG";
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
        
    }
}
