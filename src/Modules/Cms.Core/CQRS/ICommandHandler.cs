using System.Threading.Tasks;

namespace Cms.Core.CQRS
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        void Execute(TCommand command);
        Task ExecuteAsync(TCommand command);
    }

    public interface ICommandHandler<in TCommand, TResult>
       where TCommand : ICommand<TResult>
    {
        TResult Execute(TCommand command);
        Task<TResult> ExecuteAsync(TCommand command);
    }
}
