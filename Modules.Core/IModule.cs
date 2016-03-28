using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Core
{
	public interface IModule
	{
		void SetConfigurationRoot(IConfigurationRoot configurationRoot);
		void ConfigureServices(IServiceCollection services);
		void Configure(IApplicationBuilder applicationBuilder);
		void RegisterRoutes(IRouteBuilder routeBuilder);
	}
}