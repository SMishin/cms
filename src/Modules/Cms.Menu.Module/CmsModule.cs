namespace _cms.MenuModule
{
	public class CmsModule : IModule
	{
		public void SetConfigurationRoot(IConfigurationRoot configurationRoot)
		{
			
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<RazorViewEngineOptions>(options =>
			{
				options.ViewLocationExpanders.Add(new ModulesViewLocationExpander());
			});

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
