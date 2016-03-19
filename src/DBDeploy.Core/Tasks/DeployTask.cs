using System.IO;
using System.Linq;
using NLog;

namespace DBDeploy.Core.Tasks
{
	public class DeployTask : IDeployTask
	{
		private readonly IScriptExecuter _scriptExecuter;
		private readonly string _sciptsPath;

		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		public DeployTask(IScriptExecuter scriptExecuter, string sciptsPath)
		{
			_scriptExecuter = scriptExecuter;
			_sciptsPath = sciptsPath;
		}

		public virtual void Start()
		{
			ScanDirectory(_sciptsPath);
		}

		protected virtual void RunScript(string fileName)
		{
			_scriptExecuter.Execute(File.ReadAllText(fileName));
			Logger.Info(fileName);
		}

		protected virtual void ScanDirectory(string path)
		{
			foreach (var directoryPath in Directory.GetDirectories(path).OrderBy(t => t))
			{
				ScanDirectory(directoryPath);
			}

			foreach (var fileName in Directory.GetFiles(path).OrderBy(t => t))
			{
				RunScript(fileName);
			}
		}
	}
}