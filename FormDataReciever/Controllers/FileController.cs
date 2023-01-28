using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace FormDataReciever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult UploadFiles([FromForm] IEnumerable<IFormFile> files)
        {
            Console.WriteLine("hello");
            foreach(var file in files)
            {
                Console.WriteLine("found a file");
                Console.WriteLine(file.FileName);

                if(Path.GetExtension(file.FileName) == ".xlsx")
                {
                    Console.WriteLine("YES EXCEL!");
                }
            }

            return Ok();
        }
    }
}
