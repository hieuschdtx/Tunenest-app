using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tunenest.Application.Features.Roles.Queries.GetAll;

namespace tunenest.Api.Controllers
{
    [Route("api/admin/role")]
    [ApiController]
    public class RoleController : ApiControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        public RoleController(IMediator mediator, IAuthorizationService authorizationService, ILogger<RoleController> logger)
            : base(mediator, authorizationService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllRoleAsync()
        {
            var resp = await _mediator.Send(new GetAllRoleQuery());

            return Ok(resp);
        }
    }
}
