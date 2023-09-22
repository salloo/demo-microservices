namespace PostManagementService.Commands;

public class UpdatePostCommand
{
    public Guid PostId { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }

}
