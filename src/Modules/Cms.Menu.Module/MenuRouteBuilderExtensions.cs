using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;

namespace Cms.Menu.Module
{
	public static class MenuRouteBuilderExtensions
	{
		public static IRouteBuilder MapCmsRoutes(this IRouteBuilder routeBuilder)
		{
			routeBuilder.MapRoute(
				name: "menuDefault",
				template: "_cms/menu/{controller=Menu}/{action=Index}/{id?}"
			);

			return routeBuilder;
		}
	}
}
