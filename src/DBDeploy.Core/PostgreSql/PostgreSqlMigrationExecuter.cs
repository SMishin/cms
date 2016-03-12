using System.Data.Common;
using Npgsql;

namespace DBDeploy.Core.PostgreSql
{
	public sealed class PostgreSqlMigrationExecuter : DataBaseMigrationExecuter
	{
		protected override string MigrationsTableName => base.MigrationsTableName.ToLower();

		public PostgreSqlMigrationExecuter(string connectionString) : base(new NpgsqlConnection(connectionString))
		{

		}

		protected override bool IsMigrationSchemeExists()
		{
			return TableExists(MigrationsTableName);
		}

		protected override void CreateMigrationScheme()
		{
			Execute($@"CREATE TABLE public.{MigrationsTableName}
						(
						  sctipt_name varchar(4000),
						  date_time_stamp timestamp 
						)
						WITH(
						  OIDS = FALSE
						); "
					);
		}


		protected override DbCommand CreateDbCommand()
		{
			return new NpgsqlCommand();
		}

		protected override void WriteMigrationInfo(MigrationInfo migrationInfo)
		{
			using (var command = (NpgsqlCommand)GetDbCommand())
			{
				command.CommandText =
					$@"insert into public.{MigrationsTableName}
						(sctipt_name, date_time_stamp)
						VALUES (@script_name, @date_time_stamp)";

				command.Parameters.Add(new NpgsqlParameter("@script_name", NpgsqlTypes.NpgsqlDbType.Varchar) { NpgsqlValue = migrationInfo.ScriptName });
				command.Parameters.Add(new NpgsqlParameter("@date_time_stamp", NpgsqlTypes.NpgsqlDbType.Timestamp) { NpgsqlValue = migrationInfo.DateTimeStamp });

				command.ExecuteNonQuery();
			}
		}

		private bool TableExists(string tableName, string schemaName = "public")
		{
			return Exists($"select * from information_schema.tables where table_schema = '{schemaName}' and table_name = '{tableName}'");
		}

		protected override bool IsExecuted(string scriptName)
		{
			return Exists($"select * from public.{MigrationsTableName} where sctipt_name = '{scriptName}'");
		}
	}
}