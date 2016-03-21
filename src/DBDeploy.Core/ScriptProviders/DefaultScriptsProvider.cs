using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DBDeploy.Core.ScriptProviders
{
	public class DefaultScriptsProvider : IScriptsProvider
	{
		public IEnumerable<string> GetScripts(string path)
		{
			foreach (var directoryPath in Directory.GetDirectories(path).OrderBy(t => t))
			{
				foreach (var fileName in GetScripts(directoryPath))
				{
					yield return fileName;
				}
			}

			foreach (var fileName in Directory.GetFiles(path).Where(t => t.IndexOf("_index", StringComparison.Ordinal) == -1).OrderBy(t => t))
			{
				yield return fileName;
			}
		}
	}
}