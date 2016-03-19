using System;

namespace DBDeploy.Core
{
	public class MigrationInfo
	{
		public string ScriptName { get; set; }
		public string ScriptText{ get; set; }
		public string Hash { get; set; }
		public DateTime DateTimeStamp { get; set; }
		
	}
}