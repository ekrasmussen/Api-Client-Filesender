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
        public async Task<IActionResult> UploadFiles([FromForm] IEnumerable<IFormFile> files)
        {
            //Starting a loop iterating through all the files we recieved
            foreach(var file in files)
            {
                Console.WriteLine(file.FileName);

                //In our first use case, we will only be doing things if the file type is of a spreadsheet
                if(Path.GetExtension(file.FileName) == ".xlsx")
                {
                    //Making a file path to our current directory along with the filename of the file
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), file.FileName);
                    
                    //Here we create a fileStream to save it to desk at the filepath location
                    using(var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        Console.WriteLine("File should now be saved");
                    }

                    //From here on out, we can open the sheet with EPPlus, manipulate and do whatever we want with the data
                    //Do remember to remove the temporary file if you dont want it saved on the server in the future
                }
            }

            return Ok();
        }
    }
}
