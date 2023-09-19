using Microsoft.EntityFrameworkCore.Update.Internal;

namespace PostService.Model;


public class Post
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }

}