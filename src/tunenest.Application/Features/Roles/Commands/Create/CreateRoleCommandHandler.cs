using System.Net;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Commons.Interfaces.Commands;
using tunenest.Domain.Entities;
using tunenest.Domain.Enums;
using tunenest.Domain.Exceptions;
using tunenest.Domain.Extensions;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Roles.Commands.Create
{
    public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (!request.name.IsValidEnum<RoleEnum>())
                throw new BusinessRuleException("role", "Quyền người dùng không hợp lệ!",
                    HttpStatusCode.BadRequest);

            var roleName = request.name.ParseEnum<RoleEnum>();

            var role = new Role(
                request.id,
                request.name,
                request.disable,
                request.description);

            await _unitOfWork.Repository<Role, Guid>().AddAsync(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseResult(true, "Tạo quyền người dùng thành công");
        }
    }
}
