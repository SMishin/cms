using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;

namespace Modules.CMS.Web
{
	public static class CrmRouteBuilderExtensions
	{
		public static IRouteBuilder MapCmsRoutes(this IRouteBuilder routeBuilder)
		{
			routeBuilder.MapRoute(
				name: "cmsDefault",
				template: "_cms/{controller=Cms}/{action=Index}/{id?}"
			);

			return routeBuilder;
		}
	}
}
