using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Persistence;
using Application.BlogItems.Queries.Response;
using Application.BlogItems.Queries.Request;
using MediatR;
using Application.BlogItems.Commands.Response;
using Application.BlogItems.Commands.Request;
using Microsoft.AspNetCore.Authorization;



namespace AdminPanel.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        
        IMediator _mediator;
        private readonly IWebHostEnvironment _environment;

        public BlogsController(IMediator mediator, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment;
        }

        // GET: Blogs
        public async Task<IActionResult> Index(GetAllBlogQueryRequest requestModel)
        {
            List<GetAllBlogQueryResponse> allProducts = await _mediator.Send(requestModel);
            return allProducts != null ? 
                          View(allProducts) :
                          Problem("Entity set 'ApplicationDbContext.Blogs'  is null.");
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(GetByIdBlogQueryRequest requestModel)
        {
            GetByIdBlogQueryResponse blog = await _mediator.Send(requestModel);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBlogCommandRequest requestModel)
        {
            requestModel.RootPath = _environment.WebRootPath;
            if (ModelState.IsValid)
            {
                CreateBlogCommandResponse response = await _mediator.Send(requestModel);
               
            }
            return RedirectToAction("Index");
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(UpdateBlogCommandRequest requestModel)
        {
            requestModel.RootPath = _environment.WebRootPath;
            UpdateBlogCommandResponse blog = await _mediator.Send(requestModel);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateBlogCommandRequest requestModel,int a)
        {
            requestModel.RootPath = _environment.WebRootPath;
            UpdateBlogCommandResponse response = new();
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

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(GetByIdBlogQueryRequest requestModel)
        {
            GetByIdBlogQueryResponse blog = await _mediator.Send(requestModel);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(GetByIdBlogQueryRequest datas)
        {
            DeleteBlogCommandRequest delete = new();
            delete.Id = datas.Id;
            DeleteBlogCommandRequest requestModel = delete;
            requestModel.RootPath = _environment.WebRootPath;
            DeleteBlogCommandResponse response = await _mediator.Send(requestModel);
            if (response == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Blogs'  is null.");
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
