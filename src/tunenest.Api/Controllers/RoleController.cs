using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tunenest.Application.Features.Roles.Commands.Create;
using tunenest.Application.Features.Roles.Commands.Delete;
using tunenest.Application.Features.Roles.Commands.Update;
using tunenest.Application.Features.Roles.Queries.GetAll;
using tunenest.Application.Features.Roles.Queries.GetById;
using tunenest.Domain.Exceptions;
using tunenest.Domain.Extensions;

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetRoleByIdAsync([FromQuery] string id)
        {
            if (!GuidExtension.IsGuid(id))
            {
                throw new BusinessRuleException("id", "Id không phải là UUID hợp lệ.");
            }

            var resp = await _mediator.Send(new GetRoleByIdQuery(id));

            return Ok(resp);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateRoleAsync([FromBody] CreateRoleCommand command)
        {
            var resp = await _mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created, resp);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateRoleAsync([FromQuery] string id, [FromBody] UpdateRoleCommand command)
        {
            if (!GuidExtension.IsGuid(id))
            {
                throw new BusinessRuleException("id", "Id không phải là UUID hợp lệ.");
            }

            command.SetId(id);
            var resp = await _mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created, resp);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteRoleAsync([FromQuery] string id)
        {
            if (!GuidExtension.IsGuid(id))
            {
                throw new BusinessRuleException("id", "Id không phải là UUID hợp lệ.");
            }

            var resp = await _mediator.Send(new DeleteRoleCommand(id));

            return StatusCode(StatusCodes.Status201Created, resp);
        }
    }
}
