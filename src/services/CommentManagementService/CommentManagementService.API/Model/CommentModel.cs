using System.ComponentModel.DataAnnotations;

namespace CommentManagementService.Model;

public class Comment
{
    public Comment()
    {
    }
    
    [Key]
    public int Id { get; set ;}
    public Guid PostId { get; set; }
    public string Comments { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}

public class CommentViewModel
{

    public string PostId { get; set; }
    public List<string> Comments { get; set; }

}