using MediatR;

namespace UM.Application;

public interface IBaseCommand : IRequest<OperationResult>
{
}

public interface IBaseCommand<TData> : IRequest<OperationResult<TData>>
{
}