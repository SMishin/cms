using System.IO;

namespace DBDeploy.Core
{
	public class DeployTask : IDeployTask
	{
		private readonly IScriptExecuter _migrationExecuter;
		private readonly string _sciptsPath;

		public DeployTask(IScriptExecuter migrationExecuter, string sciptsPath)
		{
			_migrationExecuter = migrationExecuter;
			_sciptsPath = sciptsPath;
		}

		public virtual void Start()
		{
			foreach (var fileName in GetScripts())
			{
				RunScript(fileName);
			}
		}

		protected virtual void RunScript(string fileName)
		{
			_migrationExecuter.Execute(File.ReadAllText(fileName));
		}

		protected virtual string[] GetScripts()
		{
			return Directory.GetFiles(_sciptsPath);
		}
	}
}