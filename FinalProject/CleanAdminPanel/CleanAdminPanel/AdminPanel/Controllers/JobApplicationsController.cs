using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Persistence;
using Application.JobApplicationItems.Commands.Request;
using Application.JobApplicationItems.Commands.Response;
using Application.JobApplicationItems.Queries.Request;
using Application.JobApplicationItems.Queries.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Google.Cloud.Storage.V1;
using Google.Apis.Auth.OAuth2;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class JobApplicationsController : Controller
    {
        IMediator _mediator;
        private readonly IWebHostEnvironment _environment;
        GoogleCredential google = GoogleCredential.FromFile(@"F:\downloads\our-card-395216-aa1459032ed4.json");
        public JobApplicationsController(IMediator mediator, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment;
        }

        // GET: JobApplications
        public async Task<IActionResult> Index(GetAllJobApplicationQueryRequest requestModel)
        {
            List<GetAllJobApplicationQueryResponse> allProducts = await _mediator.Send(requestModel);
            return allProducts != null ?
                          View(allProducts) :
                          Problem("Entity set 'ApplicationDbContext.JobApplications'  is null.");
        }

        // GET: JobApplications/Details/5
        public async Task<IActionResult> Details(GetByIdJobApplicationQueryRequest requestModel)
        {
            ViewBag.Success = TempData["success"];
            GetByIdJobApplicationQueryResponse JobApplication = await _mediator.Send(requestModel);
            if (JobApplication == null)
            {
                return NotFound();
            }

            return View(JobApplication);
        }

        [HttpPost]
        public async Task<IActionResult> DownloadFile(GetByIdJobApplicationQueryResponse requestModel)
        {
            var gcsStorage = StorageClient.Create(google);
            var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
            var fileDownloadPath = Path.Combine(folderPath, requestModel.Cv);
            using (var outputFile = System.IO.File.OpenWrite(fileDownloadPath))
            {
                gcsStorage.DownloadObject("clean_admin", requestModel.Cv, outputFile);
            }
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(fileDownloadPath)
            {
                UseShellExecute = true
            };
            p.Start();
            //System.Diagnostics.Process.Start(fileDownloadPath);
            return View("Details",requestModel);
        }
            //// GET: JobApplications/Create
            //public IActionResult Create()
            //{
            //    return View();
            //}

            //// POST: JobApplications/Create
            //// To protect from overposting attacks, enable the specific properties you want to bind to.
            //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> Create(CreateJobApplicationCommandRequest requestModel)
            //{

            //    if (ModelState.IsValid)
            //    {
            //        CreateJobApplicationCommandResponse response = await _mediator.Send(requestModel);

            //    }
            //    return RedirectToAction("Index");
            //}

            //// GET: JobApplications/Edit/5
            //public async Task<IActionResult> Edit(UpdateJobApplicationCommandRequest requestModel)
            //{
            //    UpdateJobApplicationCommandResponse JobApplication = await _mediator.Send(requestModel);
            //    if (JobApplication == null)
            //    {
            //        return NotFound();
            //    }
            //    ModelState.Clear();
            //    return View(JobApplication);
            //}

            //// POST: JobApplications/Edit/5
            //// To protect from overposting attacks, enable the specific properties you want to bind to.
            //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> Edit(UpdateJobApplicationCommandRequest requestModel, int a)
            //{
            //    UpdateJobApplicationCommandResponse response = new();
            //    if (ModelState.IsValid)
            //    {
            //        try
            //        {
            //            response = await _mediator.Send(requestModel);
            //        }
            //        catch (DbUpdateConcurrencyException)
            //        {
            //            throw;
            //        }
            //        return RedirectToAction(nameof(Index));
            //    }
            //    return View(response);
            //}

            // GET: JobApplications/Delete/5
            public async Task<IActionResult> Delete(GetByIdJobApplicationQueryRequest requestModel)
        {
            GetByIdJobApplicationQueryResponse JobApplication = await _mediator.Send(requestModel);
            if (JobApplication == null)
            {
                return NotFound();
            }
            return View(JobApplication);
        }

        // POST: JobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(GetByIdJobApplicationQueryRequest datas)
        {
            DeleteJobApplicationCommandRequest delete = new();
            delete.Id = datas.Id;
            DeleteJobApplicationCommandRequest requestModel = delete;
            DeleteJobApplicationCommandResponse response = await _mediator.Send(requestModel);
            if (response == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JobApplications'  is null.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
