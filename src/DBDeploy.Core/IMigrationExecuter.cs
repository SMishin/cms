namespace DBDeploy.Core
{
	public interface IMigrationExecuter : IScriptExecuter
	{
		void WriteMigrationInfo(MigrationInfo migrationInfo);
		bool IsExecuted(string scriptName);
	}
}