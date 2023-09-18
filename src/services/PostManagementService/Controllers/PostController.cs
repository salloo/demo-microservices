using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace PostManagementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPostsAsync(int id)    
    {
        return Ok("working");
    }
}