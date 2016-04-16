using System.IO;
using NLog;

namespace DBDeploy.Core.Tasks
{
	public class DeployTask : IDeployTask
	{
		private readonly IScriptExecuter _scriptExecuter;
		private readonly IScriptsProvider _scriptsProvider;
		private readonly string _sciptsPath;

		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		public DeployTask(IScriptExecuter scriptExecuter, string sciptsPath, IScriptsProvider scriptsProvider)
		{
			_scriptExecuter = scriptExecuter;
			_sciptsPath = sciptsPath;
			_scriptsProvider = scriptsProvider;
		}

		public virtual void Start()
		{
			foreach (var fileName in _scriptsProvider.GetScripts(_sciptsPath))
			{
				RunScript(fileName);
			}
		}

		protected virtual void RunScript(string fileName)
		{
			_scriptExecuter.Execute(File.ReadAllText(fileName));
			Logger.Info(fileName);
		}
	}
}