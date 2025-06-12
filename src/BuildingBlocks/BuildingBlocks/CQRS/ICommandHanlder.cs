using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommandHanlder<in TCommand> : IRequestHandler<TCommand, Unit>
       where TCommand : ICommand<Unit>
    {
    }

    public interface ICommandHanlder<in TCommand, TResponse>: IRequestHandler<TCommand, TResponse> 
        where TCommand: ICommand<TResponse>
        where TResponse: notnull
    {
    }
}
