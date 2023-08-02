using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Persistence;
using Application.HelpItems.Commands.Request;
using Application.HelpItems.Commands.Response;
using Application.HelpItems.Queries.Request;
using Application.HelpItems.Queries.Response;
using MediatR;

namespace AdminPanel.Controllers
{
    public class HelpsController : Controller
    {
        IMediator _mediator;

        public HelpsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Helps
        public async Task<IActionResult> Index(GetAllHelpQueryRequest requestModel)
        {
            List<GetAllHelpQueryResponse> allProducts = await _mediator.Send(requestModel);
            return allProducts != null ?
                          View(allProducts) :
                          Problem("Entity set 'ApplicationDbContext.Helps'  is null.");
        }

        // GET: Helps/Details/5
        public async Task<IActionResult> Details(GetByIdHelpQueryRequest requestModel)
        {
            GetByIdHelpQueryResponse Help = await _mediator.Send(requestModel);
            if (Help == null)
            {
                return NotFound();
            }

            return View(Help);
        }

        // GET: Helps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Helps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHelpCommandRequest requestModel)
        {

            if (ModelState.IsValid)
            {
                CreateHelpCommandResponse response = await _mediator.Send(requestModel);

            }
            return RedirectToAction("Index");
        }

        // GET: Helps/Edit/5
        public async Task<IActionResult> Edit(UpdateHelpCommandRequest requestModel)
        {
            UpdateHelpCommandResponse Help = await _mediator.Send(requestModel);
            if (Help == null)
            {
                return NotFound();
            }
            ModelState.Clear();
            return View(Help);
        }

        // POST: Helps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateHelpCommandRequest requestModel, int a)
        {
            UpdateHelpCommandResponse response = new();
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

        // GET: Helps/Delete/5
        public async Task<IActionResult> Delete(GetByIdHelpQueryRequest requestModel)
        {
            GetByIdHelpQueryResponse Help = await _mediator.Send(requestModel);
            if (Help == null)
            {
                return NotFound();
            }
            return View(Help);
        }

        // POST: Helps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(GetByIdHelpQueryRequest datas)
        {
            DeleteHelpCommandRequest delete = new();
            delete.Id = datas.Id;
            DeleteHelpCommandRequest requestModel = delete;
            DeleteHelpCommandResponse response = await _mediator.Send(requestModel);
            if (response == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Helps'  is null.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
