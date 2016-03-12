using DBDeploy.App;
using DBDeploy.Console.CommandLine;
using DBDeploy.Core.PostgreSql;

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
				ScriptExecuter = new PostgreSqlMigrationExecuter(AppSettings.Configuration["Data:DefaultConnection:ConnectionString"])
			});

			app.Run(Parser.Parse(args));
		}
	}
}
