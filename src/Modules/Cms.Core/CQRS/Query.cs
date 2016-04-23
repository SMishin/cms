namespace Cms.Core.CQRS
{
    public abstract class Query<TResult> : IQuery<TResult>
    {
        public virtual string Name => GetType().Name.Replace("Query", "");

	    public virtual object GetParametersAsObject()
        {
            return new object();
        }
    }
}