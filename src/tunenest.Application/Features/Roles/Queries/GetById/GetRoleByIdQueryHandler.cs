using System.Net;
using AutoMapper;
using tunenest.Application.DTOs;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Commons.Interfaces.Queries;
using tunenest.Domain.Entities;
using tunenest.Domain.Exceptions;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Roles.Queries.GetById
{
    public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, CreateSuccessResult<RoleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateSuccessResult<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Repository<Role, Guid>().GetByIdAsync(request.id);

            if (role is null) throw new BusinessRuleException("id", "Quyền người dùng không tồn tại!", HttpStatusCode.NotFound);

            return new CreateSuccessResult<RoleDto>(_mapper.Map<RoleDto>(role), "Thành công");
        }
    }
}
