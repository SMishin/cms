using System.Collections.Generic;

namespace Modules.Core
{
	public interface IModuleManger
	{
		void Register<TModule>() where TModule : IModule, new();
		IEnumerable<IModule> GetModules();
	}
}