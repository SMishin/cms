using DBDeploy.App;
using DBDeploy.Console.CommandLine;
using DBDeploy.Core;
using DBDeploy.Core.PostgreSql;
using DBDeploy.Core.ScriptProviders;
using DBDeploy.Core.Tasks;

namespace DBDeploy.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var app = new DeployApp(new AppContext
			{
				ScriptsRootPath = AppSettings.Configuration["ScriptsRootPath"],
				SchemeScriptDirectoryName = AppSettings.Configuration["SchemeScriptDirectoryName"],
				MigrationsScriptDirectoryName = AppSettings.Configuration["MigrationsScriptDirectoryName"],
				ScriptExecuter = new PostgreSqlMigrationExecuter(AppSettings.Configuration["Data:DefaultConnection:ConnectionString"]),
				ScriptsProvider = new EntryPointScriptsProvider()
			});

			app.Run(Parser.Parse(args));
		}
	}
}
