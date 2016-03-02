using System.Data.Common;

namespace DBDeploy
{
	public interface IDeployMethod
	{
		DbConnection GetConection();
		void Execute(string script);
	}
}