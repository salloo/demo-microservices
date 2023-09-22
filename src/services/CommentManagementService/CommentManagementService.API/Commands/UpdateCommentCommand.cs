
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;

namespace CommentManagementService.Commands;

public class UpdateCommentCommand: IRequest<bool>
{
    public string PostId { get; set; }
    public string CommentId { get; set; } // comment to update

}