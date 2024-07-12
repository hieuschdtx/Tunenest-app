using AutoMapper;
using tunenest.Application.DTOs;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Commons.Interfaces.Queries;
using tunenest.Domain.Entities;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Roles.Queries.GetAll
{
    public class GetAllRoleQueryHandler : IQueryHandler<GetAllRoleQuery, CreateSuccessResult<IEnumerable<RoleDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRoleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateSuccessResult<IEnumerable<RoleDto>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Repository<Role, Guid>().GetAllAsync();

            var roles = _mapper.Map<IEnumerable<RoleDto>>(entities);

            return new CreateSuccessResult<IEnumerable<RoleDto>>(roles, "Thành công");
        }
    }
}
