using System.Data.Common;

namespace DBDeploy.Core
{
	public abstract class DataBaseMigrationExecuter : DataBaseScriptExecuter, IMigrationExecuter
	{
		protected virtual string MigrationsTableName => "Migrations";
		private bool _isMigrationSchemeExists;

		protected DataBaseMigrationExecuter(DbConnection connection) : base(connection)
		{}

		void IMigrationExecuter.WriteMigrationInfo(MigrationInfo migrationInfo)
		{
			MakeSureMigrationSchemeExist();
			WriteMigrationInfo(migrationInfo);
		}

		bool IMigrationExecuter.IsExecuted(string scriptName)
		{
			MakeSureMigrationSchemeExist();
			return IsExecuted(scriptName);
		}

		protected abstract void WriteMigrationInfo(MigrationInfo migrationInfo);
		protected abstract bool IsExecuted(string scriptName);

		protected abstract bool IsMigrationSchemeExists();
		protected abstract void CreateMigrationScheme();

		private void MakeSureMigrationSchemeExist()
		{
			if (_isMigrationSchemeExists)
			{
				return;
			}

			if (!IsMigrationSchemeExists())
			{
				CreateMigrationScheme();
			}

			_isMigrationSchemeExists = true;
		}

	}
}