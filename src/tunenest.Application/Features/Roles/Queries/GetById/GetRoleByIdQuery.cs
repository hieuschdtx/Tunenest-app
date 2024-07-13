using tunenest.Application.DTOs;
using tunenest.Domain.Commons.Interfaces.Queries;
using tunenest.Domain.Extensions;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Roles.Queries.GetById
{
    public class GetRoleByIdQuery : IQuery<CreateSuccessResult<RoleDto>>
    {
        public GetRoleByIdQuery(string id)
        {
            this.id = id.ToGuid();
        }

        public GetRoleByIdQuery()
        {
        }

        public Guid id { get; set; }
    }
}
