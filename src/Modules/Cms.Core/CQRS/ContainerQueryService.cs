using System.Threading.Tasks;

namespace Cms.Core.CQRS
{
    public interface IQueryService
    {
        TResult Query<TResult>(IQuery<TResult> query);
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }

    public class ContainerQueryService : IQueryService
    {
        private readonly IIoCWraper _ioCWraper;

        public ContainerQueryService(IIoCWraper ioCWraper)
        {
            _ioCWraper = ioCWraper;
        }

        public TResult Query<TResult>(IQuery<TResult> query)
        {
            var queryType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = _ioCWraper.GetService(queryType);
            try
            {
                return (TResult) ((dynamic) handler).Query((dynamic) query);
            }
            finally
            {
                _ioCWraper.Release(handler);
            }
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var queryType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = _ioCWraper.GetService(queryType);
            try
            {
                return (TResult)(await ((dynamic)handler).QueryAsync((dynamic)query));
            }
            finally
            {
                _ioCWraper.Release(handler);
            }
        }
    }
}