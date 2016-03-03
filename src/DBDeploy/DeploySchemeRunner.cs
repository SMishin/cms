using System.IO;

namespace DBDeploy
{
	public class DeploySchemeRunner : DeployRunner
	{
		protected DeploySchemeRunner(IScriptExecuter scriptExecuter) : base(scriptExecuter)
		{	
		}

		protected override string[] GetScripts()
		{
			return Directory.GetFiles("./DB/Scheme");
		}
	}
}