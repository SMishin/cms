using System;
using System.IO;
using System.Security.Cryptography;
using NLog;

namespace DBDeploy.Core.Tasks
{
	public class DeployMigrationsTask : DeployTask
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
		private readonly IMigrationExecuter _migrationExecuter;

		public DeployMigrationsTask(IMigrationExecuter migrationExecuter, string sciptsPath, IScriptsProvider scriptsProvider) 
			: base(migrationExecuter, sciptsPath, scriptsProvider)
		{
			_migrationExecuter = migrationExecuter;
		}

		protected override void RunScript(string fileName)
		{
			string fileHash = GetMD5HashFromFile(fileName);

			if (_migrationExecuter.IsExecuted(fileName))
			{
				var mi = _migrationExecuter.GetMigrationInfo(fileName);

				if (mi.Hash != fileHash)
				{
					Logger.Warn($"{fileName} has been changed");
				}

				return;
			}

			base.RunScript(fileName);

			_migrationExecuter.WriteMigrationInfo(new MigrationInfo
			{
				ScriptName = fileName,
				ScriptText = File.ReadAllText(fileName),
				Hash = fileHash
			});

		}

		private string GetMD5HashFromFile(string fileName)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(fileName))
				{
					return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
				}
			}
		}
	}
}