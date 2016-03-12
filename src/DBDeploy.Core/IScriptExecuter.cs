using System;

namespace DBDeploy.Core
{
	public interface IScriptExecuter : IDisposable
	{
		void Execute(string script);
		bool Exists(string script);
	}
}