using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using PostManagementService.Commands;
using PostManagementService.Repository;
using PostService.Model;

namespace PostManagementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PostsController> _logger;
    private readonly IPostRepository _repository;
    
    public PostsController(IMediator mediator, 
        ILogger<PostsController> logger,
        IPostRepository repository)
    {
        _mediator = mediator;
        _logger = logger;
        _repository = repository;
        
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetPostsByIdAsync(string postId)    
    {
        if (!Guid.TryParse(postId, out Guid postIdd)) return BadRequest("Invalid id format");
        var commentServiceUrl = $"http://localhost:5055/api/comment/{postId}";

        var result = await _repository.GetPostsAsync(postId);

        if (result == null) 
        {
            return NotFound();
        } 
        
        // make a http request to pull comments if any
        var http = new HttpClient();
        //http.BaseAddress = new Uri(commentServiceUrl);

        http.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await http.GetAsync(commentServiceUrl);


        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Success response");
            var resString = await response.Content.ReadAsStringAsync();
            _logger.LogInformation(resString);
        
            if (resString != null)
            {
                var comments = JsonConvert.DeserializeObject<CommentViewModel>(resString);
                result.Comments = comments?.Comments;
            }
        }

        // return data

        // Have to include blog posts
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePostAsync([FromBody]  CreatePostCommand command)
    {
        if (command == null) return BadRequest("Invalid object");
        _logger.LogInformation("Sending command CreatePostCommand");
        bool commandResult = await _mediator.Send(command);
        
        if (!commandResult)
        {
            return BadRequest();
        }

        return Ok("created");
    }

    // update post  and comment

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost([FromBody] UpdatePostCommand command)
    {
        // send command over Event bus
        return Ok();
    }

}