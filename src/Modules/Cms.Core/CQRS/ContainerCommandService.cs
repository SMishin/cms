using System.Threading.Tasks;

namespace Cms.Core.CQRS
{
    public interface ICommandService
    {
        void Handle(ICommand command);
        Task HandleAsync(ICommand command);
        TResult Handle<TResult>(ICommand<TResult> command);
        Task<TResult> HandleAsync<TResult>(ICommand<TResult> command);
    }

    public class ContainerCommandService : ICommandService
    {
        private readonly IIoCWraper _ioCWraper;

        public ContainerCommandService(IIoCWraper ioCWraper)
        {
            _ioCWraper = ioCWraper;
        }
        public void Handle(ICommand command)
        {
            var queryType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            var handler = _ioCWraper.GetService(queryType);
            try
            {
                ((dynamic)handler).Execute((dynamic)command);
            }
            finally
            {
                _ioCWraper.Release(handler);
            }
        }

        public async Task HandleAsync(ICommand command)
        {
            var queryType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            var handler = _ioCWraper.GetService(queryType);
            try
            {
                await ((dynamic)handler).ExecuteAsync((dynamic)command);
            }
            finally
            {
                _ioCWraper.Release(handler);
            }
        }


        public TResult Handle<TResult>(ICommand<TResult> command)
        {
            var queryType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var handler = _ioCWraper.GetService(queryType);
            try
            {
                return (TResult)((dynamic)handler).Execute((dynamic)command);
            }
            finally
            {
                _ioCWraper.Release(handler);
            }
        }

        public async Task<TResult> HandleAsync<TResult>(ICommand<TResult> command)
        {
            var queryType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var handler = _ioCWraper.GetService(queryType);
            try
            {
                return (TResult)(await ((dynamic)handler).ExecuteAsync((dynamic)command));
            }
            finally
            {
                _ioCWraper.Release(handler);
            }
        }
    }
}