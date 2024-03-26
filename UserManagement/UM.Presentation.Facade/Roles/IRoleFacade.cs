using UM.Application.Roles.Create;
using UM.Application.Roles.Edit;
using UM.Application;
using UM.Query.Roles.DTOs;
using UM.Domain.RoleAgg;

namespace UM.ServiceHost.Facade.Roles
{
    public interface IRoleFacade
    {
        //Command
        Task<OperationResult> CreateRole(CreateRoleCommand command);
        Task<OperationResult> EditRole(EditRoleCommand command);

        //Query
        Task<RoleDto?> GetRoleById(RoleId roleId);
        Task<List<RoleDto>> GetRoles();
    }
}
