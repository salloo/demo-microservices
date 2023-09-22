
using CommentManagementService.Commands;
using CommentManagementService.Model;
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
    public CommentsController(ILogger<CommentsController> logger,
        IMediator mediator)
    {
        _logger = logger; 
        _mediator = mediator;
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetCommentByPostId(string postId)
    {
        // TODO: get by id from db

        return Ok(new CommentViewModel{
            PostId = Guid.Parse(postId),
            Comments = new List<string> {
                "comment 1",
                "Comment 2"
            }
        });
    }

    [HttpPost("{postId}")]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command)
    {
        return Ok();
    }

    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand command)
    {
        return Ok();
    }
}
