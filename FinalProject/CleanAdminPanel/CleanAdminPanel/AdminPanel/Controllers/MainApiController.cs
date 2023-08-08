using Application.BlogItems.Queries.Request;
using Application.BlogItems.Queries.Response;
using Application.ContactMessageItems.Commands.Request;
using Application.ContactMessageItems.Commands.Response;
using Application.HelpItems.Queries.Request;
using Application.HelpItems.Queries.Response;
using Application.JobApplicationItems.Commands.Request;
using Application.JobApplicationItems.Commands.Response;
using Application.JobItems.Queries.Request;
using Application.JobItems.Queries.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainApiController : ControllerBase
    {

        IMediator _mediator;

        public MainApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Blog()
        {
            GetAllBlogQueryRequest requestModel = new GetAllBlogQueryRequest();
            List<GetAllBlogQueryResponse> allProducts = await _mediator.Send(requestModel);
            return allProducts != null ?
                          Ok(allProducts) :
                          Problem("Entity set 'ApplicationDbContext.Blogs'  is null.");            
        }

        [HttpGet]
        public async Task<IActionResult> BlogByID()
        {
            GetByIdBlogQueryRequest requestModel = new();
            GetByIdBlogQueryResponse blog = await _mediator.Send(requestModel);
            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }


        [HttpPost]
        public async Task<IActionResult> Contact(CreateContactMessageCommandRequest requestModel)
        {

            if (ModelState.IsValid)
            {
                CreateContactMessageCommandResponse response = await _mediator.Send(requestModel);

            }
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> Help(GetAllHelpQueryRequest requestModel)
        {
            List<GetAllHelpQueryResponse> allProducts = await _mediator.Send(requestModel);
            return allProducts != null ?
                          Ok(allProducts) :
                          Problem("Entity set 'ApplicationDbContext.Helps'  is null.");
        }


        [HttpGet]
        // GET: Helps/Details/5
        public async Task<IActionResult> HelpById(GetByIdHelpQueryRequest requestModel)
        {
            GetByIdHelpQueryResponse Help = await _mediator.Send(requestModel);
            if (Help == null)
            {
                return NotFound();
            }

            return Ok(Help);
        }


        [HttpPost]
        public async Task<IActionResult> JobApplication(CreateJobApplicationCommandRequest requestModel)
        {

            if (ModelState.IsValid)
            {
                CreateJobApplicationCommandResponse response = await _mediator.Send(requestModel);

            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Job()
        {
            GetAllJobQueryRequest requestModel = new();
            List<GetAllJobQueryResponse> allProducts = await _mediator.Send(requestModel);
            return allProducts != null ?
                          Ok(allProducts) :
                          Problem("Entity set 'ApplicationDbContext.Jobs'  is null.");
        }

        [HttpGet]
        public async Task<IActionResult> JobById()
        {
            GetByIdJobQueryRequest requestModel = new();
            GetByIdJobQueryResponse Job = await _mediator.Send(requestModel);
            if (Job == null)
            {
                return NotFound();
            }

            return Ok(Job);
        }

    }
}
