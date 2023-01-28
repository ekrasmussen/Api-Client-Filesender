// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;

// A Path to a file i want to send
string path1 = "C:\\Users\\rainf\\Desktop\\testsheet.xlsx";


//API URL
string url = "https://localhost:7185/api/File/upload";

//Making a list of all the files we want to send
//I realize i only have one path already added, but this is to illustrate how to send more than one at a time
List<string> files = new List<string>();
files.Add(path1);

using (var httpClient = new HttpClient())
{
    using (var formData = new MultipartFormDataContent())
    {
        foreach (var file in files)
        {
            //Opens a file stream with the file
            var fileStream = File.OpenRead(file);

            //StreamContent makes a HTTP Content based on a stream (filestream in this case)
            var fileContent = new StreamContent(fileStream);

            //Adding the content header saying its form data. and then also adding metadata
            //We will use the FileName on the server to see how we should handle the file based on the extension
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "files",
                FileName = Path.GetFileName(file)
            };
            //Add it to our formdata
            formData.Add(fileContent);
        }
        //Send the form data through to the API
        var response = await httpClient.PostAsync(url, formData);
    }
}