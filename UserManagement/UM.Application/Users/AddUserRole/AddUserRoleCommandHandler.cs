
using UM.Domain.UserAgg.Repository;

namespace UM.Application.Users.AddUserRole
{
    public class AddUserRoleCommandHandler : IBaseCommandHandler<AddUserRoleCommand>
    {
        private readonly IUserRepository _repository;

        public AddUserRoleCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsTrackingAsync(request.UserId);
            if (user == null)
                return OperationResult.NotFound();

            user.SetRoles(request.UserRoles);
            await _repository.SaveAsync();
            return OperationResult.Success();
        }
    }
}
