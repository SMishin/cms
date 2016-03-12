using System;

namespace DBDeploy.Console.CommandLine
{
    public static class Parser
    {
	    public static string Parse(string[] args)
	    {
		    if (args == null)
		    {
			    throw new InvalidOperationException("No parameters passed");
		    }

		    return args[0];
	    }
    }
}
