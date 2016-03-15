using System;

namespace DBDeploy.Core.Tasks
{
	public class DeployMigrationsTask : DeployTask
	{
		private readonly IMigrationExecuter _migrationExecuter;

		public DeployMigrationsTask(IMigrationExecuter migrationExecuter, string sciptsPath) : base(migrationExecuter, sciptsPath)
		{
			_migrationExecuter = migrationExecuter;
		}

		protected override void RunScript(string fileName)
		{
			if (_migrationExecuter.IsExecuted(fileName))
			{
				return;
			}

			base.RunScript(fileName);

			_migrationExecuter.WriteMigrationInfo(new MigrationInfo
			{
				 DateTimeStamp = DateTime.Now,
				 ScriptName = fileName
			});
		
		}
	}
}