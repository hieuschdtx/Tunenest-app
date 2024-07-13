using System.Net;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Commons.Interfaces.Commands;
using tunenest.Domain.Entities;
using tunenest.Domain.Exceptions;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Roles.Commands.Delete
{
    public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Repository<Role, Guid>().GetByIdAsync(request.id);

            if (role is null)
                throw new BusinessRuleException("role", "Quyền người dùng không tồn tại!", HttpStatusCode.NotFound);

            await _unitOfWork.Repository<Role, Guid>().DeleteAsync(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseResult(true, "Xóa quyền người dùng thành công");
        }
    }
}
