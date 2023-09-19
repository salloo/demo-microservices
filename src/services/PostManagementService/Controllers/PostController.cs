using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PostManagementService.Commands;
using PostService.Model;

namespace PostManagementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PostController> _logger;
    
    public PostController(IMediator mediator, ILogger<PostController> logger)
    {
        _mediator = mediator;
        _logger = logger;
        
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPostsAsync(int id)    
    {
        return Ok("working");
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody]  CreatePostCommand command)
    {
        bool commandResult = false;

        _logger.LogInformation("Sending command CreatePostCommand");

        commandResult = await _mediator.Send(command);
        
        if (!commandResult)
        {
            return BadRequest();
        }

        return Ok("created");
    }

    // update post comments

}