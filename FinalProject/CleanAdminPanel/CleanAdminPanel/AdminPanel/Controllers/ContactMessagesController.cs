using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Persistence;
using Application.ContactMessageItems.Commands.Request;
using Application.ContactMessageItems.Commands.Response;
using Application.ContactMessageItems.Queries.Request;
using Application.ContactMessageItems.Queries.Response;
using MediatR;

namespace AdminPanel.Controllers
{
    public class ContactMessagesController : Controller
    {
        IMediator _mediator;

        public ContactMessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: ContactMessages
        public async Task<IActionResult> Index(GetAllContactMessageQueryRequest requestModel)
        {
            List<GetAllContactMessageQueryResponse> allProducts = await _mediator.Send(requestModel);
            return allProducts != null ?
                          View(allProducts) :
                          Problem("Entity set 'ApplicationDbContext.ContactMessages'  is null.");
        }

        // GET: ContactMessages/Details/5
        public async Task<IActionResult> Details(GetByIdContactMessageQueryRequest requestModel)
        {
            GetByIdContactMessageQueryResponse ContactMessage = await _mediator.Send(requestModel);
            if (ContactMessage == null)
            {
                return NotFound();
            }

            return View(ContactMessage);
        }

        // GET: ContactMessages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateContactMessageCommandRequest requestModel)
        {

            if (ModelState.IsValid)
            {
                CreateContactMessageCommandResponse response = await _mediator.Send(requestModel);

            }
            return RedirectToAction("Index");
        }

        // GET: ContactMessages/Edit/5
        public async Task<IActionResult> Edit(UpdateContactMessageCommandRequest requestModel)
        {
            UpdateContactMessageCommandResponse ContactMessage = await _mediator.Send(requestModel);
            if (ContactMessage == null)
            {
                return NotFound();
            }
            ModelState.Clear();
            return View(ContactMessage);
        }

        // POST: ContactMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateContactMessageCommandRequest requestModel, int a)
        {
            UpdateContactMessageCommandResponse response = new();
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

        // GET: ContactMessages/Delete/5
        public async Task<IActionResult> Delete(GetByIdContactMessageQueryRequest requestModel)
        {
            GetByIdContactMessageQueryResponse ContactMessage = await _mediator.Send(requestModel);
            if (ContactMessage == null)
            {
                return NotFound();
            }
            return View(ContactMessage);
        }

        // POST: ContactMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(GetByIdContactMessageQueryRequest datas)
        {
            DeleteContactMessageCommandRequest delete = new();
            delete.Id = datas.Id;
            DeleteContactMessageCommandRequest requestModel = delete;
            DeleteContactMessageCommandResponse response = await _mediator.Send(requestModel);
            if (response == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ContactMessages'  is null.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
