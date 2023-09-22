
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;

namespace PostManagementService.Commands;

public class DeletePostCommand: IRequest<bool>
{
    // this should delete the corresponding comments on the other service
    // will use the event bus for this
    [DataMember]
    [Required]
    public string PostId { get; set; }

}