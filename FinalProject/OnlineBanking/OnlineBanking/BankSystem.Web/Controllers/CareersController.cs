using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using BankSystem.Web.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace BankSystem.Web.Controllers
{
    public class CareersController : Controller
    {
        HttpClientHandler clientHandler;
        HttpClient client;
        GoogleCredential google = GoogleCredential.FromFile(@"F:\downloads\our-card-395216-aa1459032ed4.json");
        private readonly IWebHostEnvironment _environment;

        public CareersController(IWebHostEnvironment environment)
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Success = TempData["success"];
            var responseMessage = await client.GetStringAsync($"https://localhost:7178/api/MainApi/Job");
            var job = JsonConvert.DeserializeObject<List<JobVM>>(responseMessage);
            JobPageVM jobPageVM = new();
            jobPageVM.VMs = job;
            return View(jobPageVM);
        }
        [HttpGet]
        public async Task<IActionResult> Position(int id)
        {
            ViewBag.Success2 = TempData["success2"];
            JobPageVM jobPageVM = new();
            var response = await client.GetAsync($"https://localhost:7178/api/MainApi/JobById/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseMessage1 = await response.Content.ReadAsStringAsync();
                JobVM jobbbb = JsonConvert.DeserializeObject<JobVM>(responseMessage1);
                jobPageVM.JobVM = jobbbb;
                return View(jobPageVM);
            }
            //var responseMessage = await client.GetStringAsync($"https://localhost:7178/api/MainApi/JobById/{id}");
            //var job = JsonConvert.DeserializeObject<JobVM>(responseMessage);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> JobApply(JobPageVM jobPageVM)
        {
           
            if (!ModelState.IsValid)
            {
                return View("Position",jobPageVM);
            }
            if (System.IO.Path.GetExtension(jobPageVM.ApplicationVM.Cv.FileName) != ".pdf")
            {
                TempData["success2"] = "false2";
                return RedirectToAction("Position", jobPageVM.JobVM);
            }
            var storage = StorageClient.Create(google);
            var bucket = storage.GetBucket("clean_admin");
            var fileName = DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(jobPageVM.ApplicationVM.Cv.FileName);
            var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
            var filePath = Path.Combine(folderPath, fileName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await jobPageVM.ApplicationVM.Cv.CopyToAsync(fileStream);
            }
            using (FileStream uploadFileStream = System.IO.File.OpenRead(filePath))
            {
                string objectName = Path.GetFileName(filePath);
                storage.UploadObject("clean_admin", objectName, null, uploadFileStream);
            }


            JobApplicationDataVm dataVM = new()
            {
                Name = jobPageVM.ApplicationVM.Name,
                Phone = jobPageVM.ApplicationVM.Phone,
                Email = jobPageVM.ApplicationVM.Email,
                Linkedin = jobPageVM.ApplicationVM.Linkedin,
                Cv = fileName
            };
            var apply = JsonConvert.SerializeObject(dataVM);
            StringContent content = new StringContent(apply, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"https://localhost:7178/api/MainApi/JobApplication", content);
            return View("Position", jobPageVM);
        }
        [HttpPost]
        public async Task<IActionResult> JobApplyFromIndex(JobPageVM jobPageVM)
        {
            if (!ModelState.IsValid || System.IO.Path.GetExtension(jobPageVM.ApplicationVM.Cv.FileName) != ".pdf")
            {
                TempData["success"] = "false";
                return RedirectToAction("Index");
            }
            var storage = StorageClient.Create(google);
            var bucket = storage.GetBucket("clean_admin");
            var fileName = DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(jobPageVM.ApplicationVM.Cv.FileName);
            var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
            var filePath = Path.Combine(folderPath, fileName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await jobPageVM.ApplicationVM.Cv.CopyToAsync(fileStream);
            }
            using (FileStream uploadFileStream = System.IO.File.OpenRead(filePath))
            {
                string objectName = Path.GetFileName(filePath);
                storage.UploadObject("clean_admin", objectName, null, uploadFileStream);
            }


            JobApplicationDataVm dataVM = new()
            {
                Name = jobPageVM.ApplicationVM.Name,
                Phone = jobPageVM.ApplicationVM.Phone,
                Email = jobPageVM.ApplicationVM.Email,
                Linkedin = jobPageVM.ApplicationVM.Linkedin,
                Cv = fileName
            };
            var apply = JsonConvert.SerializeObject(dataVM);
            StringContent content = new StringContent(apply, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"https://localhost:7178/api/MainApi/JobApplication", content);
            TempData["success"] = "true";
            return RedirectToAction("Index");
        }
    }
}
