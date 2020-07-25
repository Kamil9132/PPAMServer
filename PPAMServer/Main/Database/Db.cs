using LiteDB;
using PPAMServer.Database.Data;

namespace PPAMServer.Database
{
	class Db : Core.LiteDb.Db
	{
		public LiteCollection<Hospital> Hospitals { private set; get; }
		public LiteCollection<HospitalScore> HospitalScores { private set; get; }

		protected override string GetDatabaseName()
		{
			return "Db.db";
		}

		protected override void CreateCollections()
		{
			Hospitals = LiteDatabase.GetCollection<Hospital>("Hospitals");
			HospitalScores = LiteDatabase.GetCollection<HospitalScore>("HospitalScores");
		}

		public Db(string basePath) : base(basePath)
		{
		}
	}
}
