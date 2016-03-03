using System;
using System.Data.Common;
using Npgsql;

namespace DBDeploy
{
	public class PostgreSqlScriptExecuter : IScriptExecuter
	{
		private readonly NpgsqlConnection _connection;
		private bool _disposed;

		public PostgreSqlScriptExecuter(string connectionString)
		{
			_connection = new NpgsqlConnection(connectionString);
		}

		public DbConnection GetConection()
		{
			return _connection;
		}

		public void Execute(string script)
		{
			if (_connection.State != System.Data.ConnectionState.Open)
			{
				_connection.Open();
			}

			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = _connection;
				cmd.CommandText = script;
				cmd.ExecuteNonQuery();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing)
			{
				_connection.Dispose();
			}

			_disposed = true;
		}
	}
}