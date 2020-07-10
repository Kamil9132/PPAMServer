using Core.Network.Http.Native;
using Core.Operations;
using PPAMServer.Database;
using PPAMServer.Managers;
using System;
using System.Globalization;

namespace PPAMServer
{
	class PPAMServer
	{
		private static readonly int port = 7100;

		private static readonly string linuxMainPath = "/root/PPAMServer/";
		private static readonly string windowsMainPath = @"C:\PPAMServer\";

		private static readonly string databasePath = "Db/";

		static void Main(string[] args)
		{
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

			var mainPath = OperatingSystemOperations.IsWindowsSystem() ? windowsMainPath : linuxMainPath;
			var db = new Db(mainPath + databasePath);

			var manager = new Manager(db);

			new HttpServer(null, port, httpServerDataProviders: new Func<IDataProvider>[]
			{
				() => manager
			});
		}
	}
}
