using tunenest.Domain.Commons.Interfaces.Commands;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Roles.Commands.Delete
{
    public class DeleteRoleCommand : CommandBase<BaseResult, Guid>
    {
        public DeleteRoleCommand()
        {
        }

        public DeleteRoleCommand(string id)
        {
            SetId(id);
        }
    }
}
