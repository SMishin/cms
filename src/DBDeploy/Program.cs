using DBDeploy.CommandLine;

namespace DBDeploy
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var app = new DeployApp();
			app.Run(Parser.Parse(args));
		}
	}
}
