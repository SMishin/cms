using System.Collections.Generic;

namespace DBDeploy.Core
{
	public interface IScriptsProvider
	{
		IEnumerable<string> GetScripts(string path);
	}
}