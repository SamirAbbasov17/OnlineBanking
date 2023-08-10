using BankSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;


namespace BankSystem.Web.Controllers
{
    public class BlogsController : Controller
    {
        HttpClientHandler clientHandler;
        HttpClient client;
        GoogleCredential google = GoogleCredential.FromFile(@"F:\downloads\our-card-395216-aa1459032ed4.json");
        private readonly IWebHostEnvironment _environment;

        public BlogsController(IWebHostEnvironment environment)
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var responseMessage = await client.GetStringAsync($"https://localhost:7178/api/MainApi/Blog");
            var blog = JsonConvert.DeserializeObject<List<BlogVM>>(responseMessage);
            var gcsStorage = StorageClient.Create(google);
            foreach (var item in blog)
            {
                try
                {
                    var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
                    var fileDownloadPath = Path.Combine(folderPath, item.Image);
                    using (var outputFile = System.IO.File.OpenWrite(fileDownloadPath))
                    {
                        gcsStorage.DownloadObject("clean_admin", item.Image, outputFile);
                    }
                }
                catch (Exception)
                {

                    continue;
                }


            }
            return View(blog);
        }
        [HttpGet]
        public async Task<IActionResult> SinglePage(int id)
        {
            var responseMessage = await client.GetStringAsync($"https://localhost:7178/api/MainApi/BlogByID/{id}");
            var blog = JsonConvert.DeserializeObject<BlogVM>(responseMessage);
            return View(blog);

        }
    }
}
