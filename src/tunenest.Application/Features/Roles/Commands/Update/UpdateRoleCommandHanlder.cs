using System.Net;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Commons.Interfaces.Commands;
using tunenest.Domain.Entities;
using tunenest.Domain.Enums;
using tunenest.Domain.Exceptions;
using tunenest.Domain.Extensions;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Roles.Commands.Update
{
    public class UpdateRoleCommandHanlder : ICommandHandler<UpdateRoleCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHanlder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Repository<Role, Guid>().GetByIdAsync(request.id);

            if (role is null)
                throw new BusinessRuleException("role", "Quyền người dùng không tồn tại!", HttpStatusCode.NotFound);

            if (request.name is not null)
            {
                var isRoleName = request.name.IsValidEnum<RoleEnum>();

                if (!isRoleName)
                    throw new BusinessRuleException("role", "Quyền người dùng không hợp lệ!", HttpStatusCode.BadRequest);
            }

            PropertyUpdate.UpdateProperties<Role, UpdateRoleCommand>(role, request);

            role.CreateModifiedAt();

            await _unitOfWork.Repository<Role, Guid>().UpdateAsync(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseResult(true, "Cập nhật quyền người dùng thành công");
        }
    }
}
