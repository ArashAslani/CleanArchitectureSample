using Common.Application.FileUtil.Contracts;
using Microsoft.AspNetCore.Http;
using UM.Application.Utilities;
using UM.Domain.UserAgg.Repository;
using UM.Domain.UserAgg.Services;

namespace UM.Application.UserApplication.Edit;

internal class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;
    private readonly IFileService _fileService;
    public EditUserCommandHandler(IUserRepository repository, IUserDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAsTrackingAsync(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var oldAvatar = user.AvatarName;
        user.Edit(request.Name, request.Family, request.PhoneNumber, request.Email, request.Gender, _domainService);
        if (request.Avatar != null)
        {
            var imageName = await _fileService
                .SaveFileAndGenerateName(request.Avatar, Directories.UserAvatars);
            user.SetAvatar(imageName);
        }

        DeleteOldAvatar(request.Avatar, oldAvatar);
        await _repository.SaveAsync();
        return OperationResult.Success();
    }

    private void DeleteOldAvatar(IFormFile? avatarFile, string oldImage)
    {
        if (avatarFile == null || oldImage == "avatar.png")
            return;

        _fileService.DeleteFile(Directories.UserAvatars, oldImage);
    }
}