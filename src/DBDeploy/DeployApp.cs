using System;

namespace DBDeploy
{
	internal sealed class DeployApp
	{
		public void Run(string parameter)
		{
			var cs = AppSettings.Configuration["Data:DefaultConnection:ConnectionString"];

			switch (parameter)
			{
				case "s":
					{
						return;
					}
				case "m":
					{
						return;
					}
				default:
					{
						throw new ArgumentException($"Invalid parameter: {parameter}");
					}
			}
		}
	}
}
