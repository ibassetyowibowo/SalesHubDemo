using System.Collections;

namespace KSO_SalesHub.Utilities
{
	public class GeneralSetting
	{
		private static IDictionary env;

		internal static string AppSettingValue(string name, string key, string defValue = "")
		{
			if (Environment.GetEnvironmentVariables() != null)
			{
				env = Environment.GetEnvironmentVariables();
			}

			string _envName = "";
			if (env["ASPNETCORE_ENVIRONMENT"].ToString() == "Development")
			{
				_envName = "." + env["ASPNETCORE_ENVIRONMENT"].ToString().ToLower();
			}

			string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(projectPath)
				//.AddJsonFile($"appsettings.json")
				.AddJsonFile($"appsettings{_envName}.json")
				.Build();

			if (configuration.GetSection(name)[key] == null)
			{
				return defValue;
			}
			else
			{
				return configuration.GetSection(name)[key] ?? defValue;

			}
		}
	}
}
