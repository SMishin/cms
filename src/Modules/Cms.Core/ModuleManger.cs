using System.Collections.Generic;
using Modules.Core;

namespace Cms.Core
{
	public class ModuleManger : IModuleManger
	{
		private readonly List<IModule> _modules;

		public ModuleManger()
		{
			_modules = new List<IModule>();
		}

		public void Register<TModule>() where TModule : IModule, new()
		{
			_modules.Add(new TModule());
		}

		public IEnumerable<IModule> GetModules()
		{
			return _modules;
		}
	}
}
