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
    private readonly IConfiguration _configuration;
    
    public PostsController(IMediator mediator, 
        ILogger<PostsController> logger,
        IPostRepository repository,
        IConfiguration configuration)
    {
        _mediator = mediator;
        _logger = logger;
        _repository = repository;
        _configuration = configuration;
        
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetPostsByIdAsync(string postId)    
    {
        if (!Guid.TryParse(postId, out Guid postIdd)) return BadRequest("Invalid id format");
        var commentServiceUrl = $"{_configuration["CommentServiceUrl"]}/api/comments/{postId}"; //$"http://localhost:5055/api/comments/{postId}";

        var result = await _repository.GetPostAsync(postId);

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


        var postVM = new PostViewModel()
        {
            PostId = result.PostId.ToString(),
            Name = result.Name,
            Content = result.Content,

        };
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Success response");
            var resString = await response.Content.ReadAsStringAsync();
            _logger.LogInformation(resString);

        
            if (resString != null)
            {
                var comments = JsonConvert.DeserializeObject<CommentViewModel>(resString);
                postVM.Comments = comments?.Comments;
            }
        }

        return Ok(postVM);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePostAsync([FromBody]  CreatePostCommand command)
    {
        if (command == null) return BadRequest("missing data");
        _logger.LogInformation("Sending command CreatePostCommand");
        var createdPost = await _mediator.Send(command);

        if (createdPost == null) 
        {
            return BadRequest("something went wrong");
        }
        
        return Ok(createdPost);
    }

    // update post  and comment

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdatePost([FromBody] UpdatePostCommand command)
    //{
    //    // send command over Event bus
    //    if (command == null) return BadRequest("missing data");
    //    _logger.LogInformation("Sending command CreatePostCommand");
    //    
    //    bool commandResult = await _mediator.Send(command);
    //    
    //    if (!commandResult)
    //    {
    //        return BadRequest();
    //    }

    //    return Ok("updated");
    //}

    [HttpDelete]
    public async Task<IActionResult> DeletePost([FromBody] DeletePostCommand command)
    {
        if (command == null) return BadRequest("missing data");
        _logger.LogInformation("Sending command CreatePostCommand");
        bool commandResult = await _mediator.Send(command);
        
        if (!commandResult)
        {
            return BadRequest();
        }

        return Ok("deleted post");
    }

}