using System;

namespace DBDeploy
{
	public interface IScriptExecuter : IDisposable
	{
		void Execute(string script);
	}
}