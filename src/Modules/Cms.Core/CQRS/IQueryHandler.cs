using System.Threading.Tasks;

namespace Cms.Core.CQRS
{
	public interface IQueryHandler<in TQuery, TResult>
		where TQuery : IQuery<TResult>
	{
		TResult Query(TQuery query);
		Task<TResult> QueryAsync(TQuery query);
	}
}
