using System;
using System.Data.Common;

namespace DBDeploy.Core
{
	public abstract class DataBaseScriptExecuter : IScriptExecuter
	{
		private readonly DbConnection _connection;
		private bool _disposed;

		protected void EnsureConnectionIsOpen()
		{
			if (_connection.State != System.Data.ConnectionState.Open)
			{
				_connection.Open();
			}
		}


		protected DataBaseScriptExecuter(DbConnection connection)
		{
			_connection = connection;
		}

		public virtual void Execute(string script)
		{
			EnsureConnectionIsOpen();

			using (var cmd = CreateDbCommand())
			{
				cmd.Connection = _connection;
				cmd.CommandText = script;
				cmd.ExecuteNonQuery();
			}
		}

		public bool Exists(string script)
		{
			EnsureConnectionIsOpen();

			using (var command = GetDbCommand(script))
			using (var reader = command.ExecuteReader())
			{
				return reader.Read();
			}
		}

		protected virtual DbCommand GetDbCommand(string sqlScript)
		{
			var commmnad = GetDbCommand();
			commmnad.CommandText = sqlScript;

			return commmnad;
		}

		protected virtual DbCommand GetDbCommand()
		{
			var commmnad = CreateDbCommand();
			commmnad.Connection = _connection;

			return commmnad;
		}

		protected abstract DbCommand CreateDbCommand();

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
				_connection.Close();
				_connection.Dispose();
			}

			_disposed = true;
		}
	}
}