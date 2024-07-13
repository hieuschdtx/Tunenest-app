using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tunenest.Application.Features.Administrators.Commands.Create;
using tunenest.Application.Features.Administrators.Commands.Login;
using tunenest.Domain.Exceptions;
using tunenest.Domain.Extensions;

namespace tunenest.Api.Controllers
{
    [Route("api/admin/user")]
    [ApiController]
    public class AdministratorController : ApiControllerBase
    {
        private readonly ILogger<AdministratorController> _logger;

        public AdministratorController(IMediator mediator, IAuthorizationService authorizationService, ILogger<AdministratorController> logger)
            : base(mediator, authorizationService)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAdminAsync([FromForm] CreateAdminCommand command)
        {
            if (!command.role_id.IsGuid())
            {
                throw new BusinessRuleException("role_id", "Id không phải là UUID hợp lệ.");
            }

            var resp = await _mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created, resp);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> LoginAdminAsync([FromBody] LoginAdministratorCommand command)
        {
            var resp = await _mediator.Send(command);

            return Ok(resp);
        }
    }
}
