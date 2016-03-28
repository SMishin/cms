using System.Collections.Generic;

namespace Modules.Core
{
	public class ModuleManger : IModuleManger
	{
		private readonly List<IModule> modules;

		public ModuleManger()
		{
			modules = new List<IModule>();
		}

		public void Register<TModule>() where TModule : IModule, new()
		{
			modules.Add(new TModule());
		}

		public IEnumerable<IModule> GetModules()
		{
			return modules;
		}
	}
}
