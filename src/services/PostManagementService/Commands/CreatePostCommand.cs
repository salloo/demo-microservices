using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;

namespace PostManagementService.Commands;

public class CreatePostCommand: IRequest<bool>
{
    [Required]
    [DataMember]
    public string Name { get; set; }
    
    [Required]
    [DataMember]
    public string Content { get; set ;}
}