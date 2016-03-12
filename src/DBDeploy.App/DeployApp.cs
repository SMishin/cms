using System;
using System.Collections.Generic;
using DBDeploy.Core;

namespace DBDeploy.App
{
	public sealed class DeployApp
	{
		private readonly Dictionary<string, Action> _commands;
		private readonly IMigrationExecuter _sriptExecuter;

		public DeployApp(AppContext appContext)
		{
			_sriptExecuter = appContext.ScriptExecuter;

			_commands = new Dictionary<string, Action>
			{
				{"s", () => { new DeployTask(_sriptExecuter, appContext.SchemeScriptPath).Start(); }},
				{"m", () => { new DeployMigrationsTask(_sriptExecuter, appContext.MigrationsScriptPath).Start(); }}
			};

		}

		public void Run(string parameter)
		{
			try
			{
				if (!_commands.ContainsKey(parameter))
				{
					throw new ArgumentException($"Invalid parameter: {parameter}");
				}

				_commands[parameter].Invoke();
			}
			finally
			{
				_sriptExecuter.Dispose();
			}

		}
	}
}
