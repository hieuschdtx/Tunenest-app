using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace tunenest.Api.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IAuthorizationService _authorizationService;
        protected readonly IMediator _mediator;
        private bool? _currentIsEmployee = null;
        private string? _currentUserId = null;
        private string? _refreshToken = null;

        public ApiControllerBase(IMediator mediator, IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
        }

        //public string CurrentUserId
        //{
        //    get
        //    {
        //        if (User.Identity!.IsAuthenticated == false) return "00000000-0000-0000-0000-000000000000";

        //        return _currentUserId ??= User.FindFirstValue(ClaimTypeConst.Id) ?? "0";
        //    }
        //}

        // public string CurrentRefreshToken
        // {
        //     get
        //     {
        //         if (User.Identity!.IsAuthenticated == false) return "0";
        //
        //         return _refreshToken ??= User.FindFirstValue(("refresh_token")) ?? "0";
        //     }
        // }

        //public bool IsEmployee
        //{
        //    get
        //    {
        //        if (User.Identity!.IsAuthenticated == false) return false;
        //        if (_currentIsEmployee.HasValue) return _currentIsEmployee.Value;
        //        _currentIsEmployee = User.HasClaim(ClaimTypes.Role, RoleConst.Employee) &&
        //                             (User.IsInRole(RoleConst.Administrator) || User.IsInRole(RoleConst.Manager));
        //        return _currentIsEmployee.Value;
        //    }
        //}
    }
}
