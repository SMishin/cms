using DBDeploy.Core;

namespace DBDeploy.App
{
    public class AppContext
    {
	    public IMigrationExecuter ScriptExecuter { get; set; }

		public string ScriptsRootPath { get; set; }
		public string SchemeScriptDirectoryName { get; set; }
		public string MigrationsScriptDirectoryName { get; set; }
		public string SchemeScriptPath => System.IO.Path.Combine(ScriptsRootPath, SchemeScriptDirectoryName);
		public string MigrationsScriptPath => System.IO.Path.Combine(ScriptsRootPath, MigrationsScriptDirectoryName);
    }
}
