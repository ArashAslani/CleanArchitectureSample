using MediatR;
using UM.Application.Roles.Create;
using UM.Application.Roles.Edit;
using UM.Application;
using UM.Query.Roles.DTOs;
using UM.Query.Roles.GetById;
using UM.Query.Roles.GetList;
using UM.Domain.RoleAgg;
using Common.DotNetCore.Utilities.DependencyInjection;

namespace UM.ServiceHost.Facade.Roles
{
    public class RoleFacade : IRoleFacade, IScopedDependency
    {
        private readonly IMediator _mediator;

        public RoleFacade(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<OperationResult> CreateRole(CreateRoleCommand command)
        {
            return await _mediator.Send(command);
        }
        public async Task<OperationResult> EditRole(EditRoleCommand command)
        {
            return await _mediator.Send(command);
        }
        public async Task<RoleDto?> GetRoleById(RoleId roleId)
        {
            return await _mediator.Send(new GetRoleByIdQuery(roleId));
        }
        public async Task<List<RoleDto>> GetRoles()
        {
            return await _mediator.Send(new GetRoleListQuery());
        }
    }
}
