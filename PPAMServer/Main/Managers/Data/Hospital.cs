using PPAMServer.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PPAMServer.Managers.Data
{
	class Hospital : Database.Data.Hospital
	{
		public List<HospitalScore> Scores { get; }
		public List<HospitalComment> Comments { get; }

		public Hospital(Database.Data.Hospital hospital, IEnumerable<HospitalScore> scores, IEnumerable<HospitalComment> comments)
			: base(hospital)
		{
			Scores = scores.ToList();
			Comments = comments.ToList();
		}
	}
}
