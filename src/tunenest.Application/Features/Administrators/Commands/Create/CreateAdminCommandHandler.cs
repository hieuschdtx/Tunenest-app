using System.Net;
using tunenest.Application.Services.AdminServices;
using tunenest.Application.Services.CloudinaryServices;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Commons.Interfaces.Commands;
using tunenest.Domain.Consts;
using tunenest.Domain.Entities;
using tunenest.Domain.Entities.Administrators;
using tunenest.Domain.Exceptions;
using tunenest.Domain.Extensions;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Administrators.Commands.Create
{
    public class CreateAdminCommandHandler : ICommandHandler<CreateAdminCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IAdminService _adminService;

        public CreateAdminCommandHandler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService, IAdminService adminService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
            _adminService = adminService;
        }

        public async Task<BaseResult> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var avatarUrl = string.Empty;

            Guid roleId = request.role_id.ToGuid();

            if (await _adminService.ExistEmailAsync(request.email))
                throw new BusinessRuleException("email", "Email đã tồn tại.", HttpStatusCode.BadRequest);

            if (await _adminService.ExistPhoneNumberAsync(request.phone_number))
                throw new BusinessRuleException("phone_number", "Số điện thoại đã tồn tại.", HttpStatusCode.BadRequest);

            if (await _unitOfWork.Repository<Role, Guid>().GetByIdAsync(roleId) is null)
                throw new BusinessRuleException("role_id", "Quyền người dùng không tồn tại.", HttpStatusCode.BadRequest);

            if (request.avatar_file is not null)
            {
                try
                {
                    var imageUrl =
                        await _cloudinaryService.UploadImageAsync(request.avatar_file, CloudinaryFolderConst.Admins);
                    avatarUrl = imageUrl.SecureUrl.ToString();
                }
                catch (Exception)
                {
                    throw new BusinessRuleException("avatar_file", "Tải lên hình ảnh thất bại.",
                        HttpStatusCode.InternalServerError);
                }
            }

            var admin = new Administrator(
                request.id,
                request.user_name,
                request.email,
                request.phone_number,
                request.dob,
                request.gender,
                string.IsNullOrEmpty(avatarUrl) ? null : avatarUrl,
                request.password,
                request.permission.ToJson(),
                request.access_failed_count,
                roleId,
                request.lock_acc);

            await _unitOfWork.Repository<Administrator, Guid>().AddAsync(admin);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseResult(true, "Tạo tài khoản quản trị viên thành công.");
        }
    }
}
