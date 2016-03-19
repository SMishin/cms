using System.Data.Common;
using Npgsql;

namespace DBDeploy.Core.PostgreSql
{
	public sealed class PostgreSqlMigrationExecuter : DataBaseMigrationExecuter
	{
		protected override string MigrationsTableName => base.MigrationsTableName.ToLower();

		public PostgreSqlMigrationExecuter(string connectionString) : base(new NpgsqlConnection(connectionString))
		{}

		protected override bool IsMigrationSchemeExists()
		{
			return TableExists(MigrationsTableName);
		}

		protected override void CreateMigrationScheme()
		{
			Execute($@"CREATE TABLE public.{MigrationsTableName}
						(
						  sctipt_name varchar(4000),
						  script_text text,
						  hash text,
						  date_time_stamp timestamp without time zone default (now() at time zone 'utc')
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

		public override MigrationInfo GetMigrationInfo(string scriptName)
		{
			using (var command = (NpgsqlCommand)GetDbCommand())
			{
				command.CommandText =
					$@" select * from public.{MigrationsTableName}
						where sctipt_name = @script_name";

				command.Parameters.Add(new NpgsqlParameter("@script_name", NpgsqlTypes.NpgsqlDbType.Varchar) { NpgsqlValue = scriptName });

				using (var reader = command.ExecuteReader())
				{
					if (!reader.Read())
					{
						return null;
					}

					MigrationInfo mi = new MigrationInfo
					{
						ScriptName = reader.GetString(0),
						ScriptText = reader.GetString(1),
						Hash = reader.GetString(2),
						DateTimeStamp = reader.GetDateTime(3)
					};

					return mi;
				}
			}
		}

		protected override void WriteMigrationInfo(MigrationInfo migrationInfo)
		{
			using (var command = (NpgsqlCommand)GetDbCommand())
			{
				command.CommandText =
					$@"insert into public.{MigrationsTableName}
						(sctipt_name, script_text, hash)
						VALUES (@sctipt_name, @script_text, @hash)";

				command.Parameters.Add(new NpgsqlParameter("@sctipt_name", NpgsqlTypes.NpgsqlDbType.Varchar) { NpgsqlValue = migrationInfo.ScriptName });
				command.Parameters.Add(new NpgsqlParameter("@script_text", NpgsqlTypes.NpgsqlDbType.Text) { NpgsqlValue = migrationInfo.ScriptText });
				command.Parameters.Add(new NpgsqlParameter("@hash", NpgsqlTypes.NpgsqlDbType.Text) { NpgsqlValue = migrationInfo.Hash });

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