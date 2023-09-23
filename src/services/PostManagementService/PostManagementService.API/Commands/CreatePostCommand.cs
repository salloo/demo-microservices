using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;
using PostService.Model;

namespace PostManagementService.Commands;

public class CreatePostCommand: IRequest<Post>
{
    [Required]
    [DataMember]
    public string Name { get; set; }
    
    [Required]
    [DataMember]
    public string Content { get; set ;}
}