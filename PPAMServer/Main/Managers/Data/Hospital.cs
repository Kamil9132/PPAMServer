using PPAMServer.Database.Data;
using System.Collections.Generic;
using System.Linq;

namespace PPAMServer.Managers.Data
{
	class Hospital : Database.Data.Hospital
	{
		public List<HospitalScore> Scores { get; }

		public Hospital(Database.Data.Hospital hospital, IEnumerable<HospitalScore> scores)
			: base(hospital)
		{
			Scores = scores.ToList();
		}
	}
}
