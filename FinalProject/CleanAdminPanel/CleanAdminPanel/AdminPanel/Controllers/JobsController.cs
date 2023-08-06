using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Persistence;
using Application.JobItems.Commands.Request;
using Application.JobItems.Commands.Response;
using Application.JobItems.Queries.Request;
using Application.JobItems.Queries.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        IMediator _mediator;

        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Jobs
        public async Task<IActionResult> Index(GetAllJobQueryRequest requestModel)
        {
            List<GetAllJobQueryResponse> allProducts = await _mediator.Send(requestModel);
            return allProducts != null ?
                          View(allProducts) :
                          Problem("Entity set 'ApplicationDbContext.Jobs'  is null.");
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(GetByIdJobQueryRequest requestModel)
        {
            GetByIdJobQueryResponse Job = await _mediator.Send(requestModel);
            if (Job == null)
            {
                return NotFound();
            }

            return View(Job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateJobCommandRequest requestModel)
        {

            CreateJobCommandResponse response = await _mediator.Send(requestModel);

            return RedirectToAction("Index");
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(UpdateJobCommandRequest requestModel)
        {
            UpdateJobCommandResponse Job = await _mediator.Send(requestModel);
            if (Job == null)
            {
                return NotFound();
            }
            ModelState.Clear();
            return View(Job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateJobCommandRequest requestModel, int a)
        {
            UpdateJobCommandResponse response = new();
            if (ModelState.IsValid)
            {
                try
                {
                    response = await _mediator.Send(requestModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(response);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(GetByIdJobQueryRequest requestModel)
        {
            GetByIdJobQueryResponse Job = await _mediator.Send(requestModel);
            if (Job == null)
            {
                return NotFound();
            }
            return View(Job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(GetByIdJobQueryRequest datas)
        {
            DeleteJobCommandRequest delete = new();
            delete.Id = datas.Id;
            DeleteJobCommandRequest requestModel = delete;
            DeleteJobCommandResponse response = await _mediator.Send(requestModel);
            if (response == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jobs'  is null.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
