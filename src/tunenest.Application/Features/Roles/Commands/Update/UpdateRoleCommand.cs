using tunenest.Domain.Commons.Interfaces.Commands;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Roles.Commands.Update
{
    public class UpdateRoleCommand : CommandBase<BaseResult, Guid>
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public bool? disable { get; set; }
    }
}
