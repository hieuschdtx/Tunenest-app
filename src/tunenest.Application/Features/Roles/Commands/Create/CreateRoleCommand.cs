using tunenest.Domain.Commons.Interfaces.Commands;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Roles.Commands.Create
{
    public class CreateRoleCommand : CommandBase<BaseResult, Guid>
    {
        public string name { get; set; }
        public string? description { get; set; }
        public bool disable { get; set; } = false;
    }
}
