using System.IO;
using Npgsql;

namespace DBDeploy
{
	public class Program
	{
		public static void Main(string[] args)
		{
			foreach (var fileName in Directory.GetFiles("./DB/Scheme"))
			{
				using (var conn = new NpgsqlConnection(AppSettings.Configuration["Data:DefaultConnection:ConnectionString"]))
				{
					conn.Open();
					using (var cmd = new NpgsqlCommand())
					{
						cmd.Connection = conn;

						// Insert some data
						cmd.CommandText = File.ReadAllText(fileName);
						cmd.ExecuteNonQuery();
					}
				}
			}
		}
	}
}
