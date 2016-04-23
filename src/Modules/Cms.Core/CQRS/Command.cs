namespace Cms.Core.CQRS
{
    public abstract class Command : ICommand
    {
        public virtual string Name => GetType().Name.Replace("Command", "");

	    public virtual object GetParametersAsObject()
        {
            return new object();
        }
    }

    public abstract class Command<TResult> : Command, ICommand<TResult>
    {
    }
}