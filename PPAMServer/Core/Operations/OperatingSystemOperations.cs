using System;

namespace Core.Operations
{
	class OperatingSystemOperations
	{
		public static void RestartSystem()
		{
			var process = ProcessOperations.ExecuteProcess("cmd", "/C shutdown -f -r", createNoWindow: true);

			process.WaitForExit();
		}

		public static bool IsWindowsSystem()
		{
			var platform = (int)Environment.OSVersion.Platform;

			return platform != 4 && platform != 6 && platform != 128;
		}
	}
}
