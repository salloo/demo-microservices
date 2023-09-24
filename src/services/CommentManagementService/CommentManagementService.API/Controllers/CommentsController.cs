
using CommentManagementService.Commands;
using CommentManagementService.Model;
using CommentManagementService.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;

namespace CommentManagementService.Controllers;


[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("sliding")]
public class CommentsController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CommentsController> _logger;
    private readonly ICommentRepository _repository;
    public CommentsController(ILogger<CommentsController> logger,
        IMediator mediator,
        ICommentRepository repository)
    {
        _logger = logger; 
        _mediator = mediator;
        _repository = repository;
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetCommentByPostId(string postId)
    {
        if (postId == null) return BadRequest();
        var result = await _repository.GetCommentsAsync(postId);
        if (!result.Comments.Any()) 
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command)
    {
        if (command == null) return BadRequest("Invalid data");
        _logger.LogInformation("Sending create comment command");
        var created = await _mediator.Send(command);

        if (!created)
        {
            return BadRequest("not able to create comment");
        }

        return Accepted();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand command)
    {
        if (command == null) return BadRequest("Invalid data");
        _logger.LogInformation("Sending update comment command");

        var result = await _mediator.Send(command);

        if (!result)
        {
            return BadRequest("not able to update comment");
        }
        
        return Accepted();
    }
}
