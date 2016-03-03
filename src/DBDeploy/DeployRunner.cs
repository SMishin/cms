using System.IO;

namespace DBDeploy
{
	public abstract class DeployRunner : IDeployRunner
	{
		private readonly IScriptExecuter _scriptExecuter;

		protected DeployRunner(IScriptExecuter scriptExecuter)
		{
			_scriptExecuter = scriptExecuter;
		}

		public virtual void Start()
		{
			foreach (var fileName in GetScripts())
			{
				_scriptExecuter.Execute(File.ReadAllText(fileName));
			}

			_scriptExecuter.Dispose();

		}

		protected abstract string[] GetScripts();
	}
}