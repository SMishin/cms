using System.IO;
using System.Linq;
using NLog;

namespace DBDeploy.Core.Tasks
{
	public class DeployTask : IDeployTask
	{
		private readonly IScriptExecuter _migrationExecuter;
		private readonly string _sciptsPath;

		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		public DeployTask(IScriptExecuter migrationExecuter, string sciptsPath)
		{
			_migrationExecuter = migrationExecuter;
			_sciptsPath = sciptsPath;
		}

		public virtual void Start()
		{
			RunFromDirectory(_sciptsPath);
		}

		protected virtual void RunScript(string fileName)
		{
			_migrationExecuter.Execute(File.ReadAllText(fileName));
		}

		protected virtual void RunFromDirectory(string path)
		{
			foreach (var directoryPath in Directory.GetDirectories(path).OrderBy(t => t))
			{
				RunFromDirectory(directoryPath);
			}

			foreach (var fileName in Directory.GetFiles(path).OrderBy(t => t))
			{
				Logger.Info(fileName);
				//RunScript(fileName);
			}
		}
	}
}