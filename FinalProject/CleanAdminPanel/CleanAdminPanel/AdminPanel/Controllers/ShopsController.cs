using AdminPanel.ViewModels;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using Newtonsoft.Json;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace AdminPanel.Controllers
{


    [Authorize]
    public class ShopsController : Controller
    {
        GoogleCredential google = GoogleCredential.FromFile(@"F:\downloads\our-card-395216-aa1459032ed4.json");
        private readonly IWebHostEnvironment _environment;
        HttpClientHandler clientHandler;
        HttpClient client;

        public ShopsController(IWebHostEnvironment environment)
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var responseMessage = await client.GetStringAsync("https://localhost:7098/items");
            var shopVM = JsonConvert.DeserializeObject<List<ShopDataVM>>(responseMessage);
            return View(shopVM);
        }
        public IActionResult Create()
        {
			ViewBag.Success = TempData["success"];
			return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ShopCreateVM shopVM)
        {
			if (shopVM.Image == null || System.IO.Path.GetExtension(shopVM.Image.FileName) != ".jpeg" || System.IO.Path.GetExtension(shopVM.Image.FileName) != ".jpg" || System.IO.Path.GetExtension(shopVM.Image.FileName) != ".png")
			{

				TempData["success"] = "false";
				return RedirectToAction("Create");
			}
			var storage = StorageClient.Create(google);
            var bucket = storage.GetBucket("clean_admin");
            var fileName = DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(shopVM.Image.FileName);
            var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
            var filePath = Path.Combine(folderPath, fileName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await shopVM.Image.CopyToAsync(fileStream);
            }      
            using (FileStream uploadFileStream = System.IO.File.OpenRead(filePath))
            {
                string objectName = Path.GetFileName(filePath);
                storage.UploadObject("clean_admin", objectName, null, uploadFileStream);
            }
            ShopCreateDataVM shopDataVM = new()
            {
                Name = shopVM.Name,
                Description = shopVM.Description,
                Image = fileName,
                Price = shopVM.Price
            };
            var add = JsonConvert.SerializeObject(shopDataVM);
            StringContent content = new StringContent(add, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7098/items", content);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(string id)
        {
          
            var gcsStorage = StorageClient.Create(google);
            var responseMessage = await client.GetStringAsync($"https://localhost:7098/items/{id}");
            var shopVM = JsonConvert.DeserializeObject<ShopDataVM>(responseMessage);
            var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
            var fileDownloadPath = Path.Combine(folderPath, shopVM.Image);
            using var outputFile = System.IO.File.OpenWrite(fileDownloadPath);
            gcsStorage.DownloadObject("clean_admin", shopVM.Image, outputFile);
            return View(shopVM);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var responseMessage = await client.GetStringAsync($"https://localhost:7098/items/{id}");
            var shopVM = JsonConvert.DeserializeObject<ShopDataVM>(responseMessage);
            return View(shopVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ShopDataVM shopDataVM)
        {
            var storage = StorageClient.Create(google);
            var testData = await client.GetStringAsync($"https://localhost:7098/items/{shopDataVM.Id}");
            var shop = JsonConvert.DeserializeObject<ShopDataVM>(testData);
            var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
            var path = Path.Combine(folderPath, shop.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                storage.DeleteObject("clean_admin", shop.Image);
            }

            var responseMessage = await client.DeleteAsync($"https://localhost:7098/items/{shopDataVM.Id}");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(string id)
        {
			ViewBag.Success = TempData["success"];
			var responseMessage = await client.GetStringAsync($"https://localhost:7098/items/{id}");
            var shopDataVM = JsonConvert.DeserializeObject<ShopDataVM>(responseMessage);
            ShopVM shopVM = new()
            {
                Id = shopDataVM.Id,
                Name = shopDataVM.Name,
                Description = shopDataVM.Description,
                Price = shopDataVM.Price,
                Image = null
            };
            return View(shopVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ShopVM shopVM)
        {
			if (shopVM.Image != null)
			{
				if (System.IO.Path.GetExtension(shopVM.Image.FileName) != ".jpeg" || System.IO.Path.GetExtension(shopVM.Image.FileName) != ".jpg" || System.IO.Path.GetExtension(shopVM.Image.FileName) != ".png")
				{

					TempData["success"] = "false";
					return RedirectToAction("Update");
				}
			}

			ShopDataVM shopDataVM = new();
            if (shopVM.Image != null)
            {
                var testData = await client.GetStringAsync($"https://localhost:7098/items/{shopVM.Id}");
                var shop = JsonConvert.DeserializeObject<ShopDataVM>(testData);
                var storage = StorageClient.Create(google);
                var bucket = storage.GetBucket("clean_admin");
                var fileName = DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(shopVM.Image.FileName);
                var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
                var filePath = Path.Combine(folderPath, fileName);
                var deletePath = Path.Combine(folderPath, shop.Image);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                    storage.DeleteObject("clean_admin", shop.Image);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await shopVM.Image.CopyToAsync(fileStream);
                }
                using (FileStream uploadFileStream = System.IO.File.OpenRead(filePath))
                {
                    string objectName = Path.GetFileName(filePath);
                    storage.UploadObject("clean_admin", objectName, null, uploadFileStream);
                }
                shopDataVM = new()
                {
                    Id = shopVM.Id,
                    Name = shopVM.Name,
                    Description = shopVM.Description,
                    Image = fileName,
                    Price = shopVM.Price
                };
            }
            else
            {
                var testData = await client.GetStringAsync($"https://localhost:7098/items/{shopVM.Id}");
                var shop = JsonConvert.DeserializeObject<ShopDataVM>(testData);
                shopDataVM = new()
                {
                    Id = shopVM.Id,
                    Name = shopVM.Name,
                    Description = shopVM.Description,
                    Image = shop.Image,
                    Price = shopVM.Price
                };

            }
            var add = JsonConvert.SerializeObject(shopDataVM);
            StringContent content = new StringContent(add, Encoding.UTF8, "application/json"); 
            var responseMessage = await client.PutAsync($"https://localhost:7098/items/{shopVM.Id}", content);  
            return RedirectToAction("Index");
        }
    }
}
