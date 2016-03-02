using System;
using System.Data.Common;
using Npgsql;

namespace DBDeploy
{
	public class PostgreSqlDeployMethod : IDeployMethod, IDisposable
	{
		private readonly NpgsqlConnection _connection;

		public PostgreSqlDeployMethod(string connectionString)
		{
			_connection = new NpgsqlConnection(connectionString);
		}

		public DbConnection GetConection()
		{
			return _connection;
		}

		public void Execute(string script)
		{
			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = _connection;

				// Insert some data
				cmd.CommandText = script;
				cmd.ExecuteNonQuery();
			}
		}

		public void Dispose()
		{
			_connection.Dispose();
		}
	}
}