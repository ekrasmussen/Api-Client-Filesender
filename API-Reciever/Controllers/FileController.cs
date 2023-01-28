using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OfficeOpenXml;

namespace API_Reciever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult UploadExcelFile()
        {
            // Receive the byte array from the body of the HTTP POST request
            if(Request.ContentLength > 0)
            {
                var data = new byte[Request.ContentLength.Value];
                Request.Body.ReadAsync(data, 0, data.Length);

                // Save the byte array to a file
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploaded.xlsx");
                System.IO.File.WriteAllBytes(filePath, data);

                // Read the data from the Excel file using EPPlus
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var cellA1 = worksheet.Cells["A1"].Value; // "Hello"
                    package.Save();
                    Console.WriteLine(cellA1);
                }
            }

            return Ok();
        }
    }
}
