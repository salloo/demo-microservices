
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;

namespace CommentManagementService.Commands;

public class CreateCommentCommand: IRequest<bool>
{
    [DataMember]
    public string postId { get; set; }

    [DataMember]
    public string Content { get; set; }
}