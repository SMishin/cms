using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;

namespace DBDeploy.Core.ScriptProviders
{
	public class EntryPointScriptsProvider : IScriptsProvider
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
		private string indexFileName = "_index.sql";

		public IEnumerable<string> GetScripts(string path)
		{
			var indexFile = File.Exists(path)
				? path
				: Directory.GetFiles(path).First(t => t.IndexOf(indexFileName, StringComparison.Ordinal) != -1);

			var files = File.ReadAllLines(indexFile)
				.Select(ParseLine)
				.Where(t => t != "")
				.ToArray()
				;

			foreach (var fileName in files)
			{
				if (fileName.IndexOf(indexFileName, StringComparison.Ordinal) != -1)
				{
					foreach (var file in GetScripts(Path.Combine(path, fileName).Replace(indexFileName, "")))
					{
						yield return file;
					}
				}
				else
				{
					yield return Path.GetFullPath(Path.Combine(path, fileName)).Replace(Directory.GetCurrentDirectory(), @".");
				}
			}
		}

		private string ParseLine(string line)
		{
			if (line == "")
			{
				return line;
			}

			var match = System.Text.RegularExpressions.Regex.Match(line, ":r (.*)");

			if (match.Success)
			{
				return match.Groups[1].Value;
			}

			Logger.Warn($"Ignored line with incorrect format: {line}");
			return "";
		}
	}
}