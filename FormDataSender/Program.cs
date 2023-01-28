// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;
Console.ReadKey();
Console.WriteLine("Hello, World!");

string path1 = "C:\\Users\\rainf\\Desktop\\testsheet.xlsx";
string url = "https://localhost:7185/api/File/upload";

//using (var client = new HttpClient())
//{
//    using (var content = new MultipartFormDataContent())
//    {
//        // Add the files to the content
//        var file1 = new ByteArrayContent(File.ReadAllBytes(path1));
//        file1.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
//        {
//            FileName = "testsheet.xlsx"
//        };
//        content.Add(file1);

//        // Send the request
//        var result = await client.PostAsync(url, content);
//        Console.WriteLine(result);
//    }
//}
List<string> files = new List<string>();
files.Add(path1);
files.Add(path1);
using (var httpClient = new HttpClient())
{
    using (var formData = new MultipartFormDataContent())
    {
        foreach (var file in files)
        {
            var fileStream = File.OpenRead(file);
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "files",
                FileName = Path.GetFileName(file)
            };
            formData.Add(fileContent);
        }
        var response = await httpClient.PostAsync(url, formData);
    }
}