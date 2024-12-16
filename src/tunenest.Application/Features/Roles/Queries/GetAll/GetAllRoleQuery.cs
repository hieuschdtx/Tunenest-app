using tunenest.Application.DTOs;
using tunenest.Domain.Commons.Interfaces.Queries;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Roles.Queries.GetAll
{
    public class GetAllRoleQuery : IQuery<CreateSuccessResult<IEnumerable<RoleDto>>>
    {
        public GetAllRoleQuery()
        { }
    }
}
