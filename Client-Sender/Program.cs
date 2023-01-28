// See https://aka.ms/new-console-template for more information
using OfficeOpenXml;
using System.Net.Http.Headers;
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
Console.ReadKey();
Console.WriteLine("Start");
string path = "C:\\Users\\rainf\\Desktop\\testsheet.xlsx";
string url = "https://localhost:7077/api/File/upload";

FileInfo existingFile = new FileInfo(path);


using (var package = new ExcelPackage(existingFile))
{
    // Create the worksheet and populate it with data
    var worksheet = package.Workbook.Worksheets["Sheet1"];
    worksheet.Cells["A1"].Value = "YourMom";

    // Convert the Excel package to a byte array
    var data = package.GetAsByteArray();

    // Send the byte array to the API in the body of an HTTP POST request
    using (var client = new HttpClient())
    {
        var content = new ByteArrayContent(data);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        var response = client.PostAsync(url, content).Result;
        Console.WriteLine(response.StatusCode);
    }
}