
using CommentManagementService.Commands;
using CommentManagementService.Model;
using CommentManagementService.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CommentManagementService.Controllers;


[ApiController]
[Route("api/[controller]")]
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
        var result = _repository.GetComments(postId);
        if (!result.Comments.Any()) 
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command)
    {
        if (command == null) return BadRequest("missing data");
        _logger.LogInformation("Sending create comment command");
        var created = await _mediator.Send(command);

        if (!created)
        {
            return BadRequest("something went wrong");
        }

        return Accepted();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand command)
    {
        return Ok("Todo");
    }
}
