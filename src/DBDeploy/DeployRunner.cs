using System.IO;

namespace DBDeploy
{
	public abstract class DeployRunner : IDeployRunner
	{
		private readonly IDeployMethod _deployMethod;

		protected DeployRunner(IDeployMethod deployMethod)
		{
			_deployMethod = deployMethod;
		}

		public virtual void Start()
		{
			using (var connection = _deployMethod.GetConection())
			{
				connection.Open();

				foreach (var fileName in GetScripts())
				{
					_deployMethod.Execute(File.ReadAllText(fileName));
				}
			}
		}

		protected abstract string[] GetScripts();
	}
}