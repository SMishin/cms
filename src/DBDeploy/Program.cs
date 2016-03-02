using System;
using System.IO;
using DBDeploy.CommandLine;
using Npgsql;

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
