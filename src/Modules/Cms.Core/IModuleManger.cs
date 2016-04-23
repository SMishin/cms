using System.Collections.Generic;

namespace Cms.Core
{
	public interface IModuleManger
	{
		void Register<TModule>() where TModule : IModule, new();
		IEnumerable<IModule> GetModules();
	}
}