using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Core;

namespace Modules.CMS.Web
{
	public class CmsModule : IModule
	{

		public void SetConfigurationRoot(IConfigurationRoot configurationRoot)
		{
			
		}

		public void ConfigureServices(IServiceCollection services)
		{
			
		}

		public void Configure(IApplicationBuilder applicationBuilder)
		{
			
		}

		public void RegisterRoutes(IRouteBuilder routeBuilder)
		{
			routeBuilder.MapCmsRoutes();
		}
	}
}
