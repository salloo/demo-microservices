using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace PostService.Model;


public class Post
{
    [Key]
    public int Id { get; set; }
    public Guid PostId { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }

}

public class Comment 
{
    public Comment()
    {
        Comments = new List<string>();
    }
    public Guid PostId { get; set; }
    public List<string> Comments { get; set; }

}

public class PostViewModel 
{

    public string PostId { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public List<string> Comments { get; set; }
}

public class CommentViewModel
{

    public Guid PostId { get; set; }
    public List<string> Comments { get; set; }

}