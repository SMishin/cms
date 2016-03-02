using System.IO;

namespace DBDeploy
{
	public class DeploySchemeRunner : DeployRunner
	{
		protected DeploySchemeRunner(IDeployMethod deployMethod) : base(deployMethod)
		{	
		}

		protected override string[] GetScripts()
		{
			return Directory.GetFiles("./DB/Scheme");
		}
	}
}