namespace CommentService.Model;

public class Comment
{
    public Comment()
    {
       Comments = new List<string>(); 
    }

    public Guid PostId { get; set; }
    public List<string> Comments { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}

public class CommentViewModel
{

    public Guid PostId { get; set; }
    public List<string> Comments { get; set; }

}