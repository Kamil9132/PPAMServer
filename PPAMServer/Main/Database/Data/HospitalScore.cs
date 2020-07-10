using LiteDB;
using System;

namespace PPAMServer.Database.Data
{
	class HospitalScore
	{
		public class Key
		{
			public Guid HospitalId { get; set; }
			public string User { get; set; }

			public Key()
			{

			}

			public Key(Guid hospitalId, string user)
			{
				HospitalId = hospitalId;
				User = user;
			}

			public bool IsEqual(Key key)
			{
				return HospitalId == key.HospitalId
					&& User == key.User;
			}
		}

		[BsonId]
		public Key Id { get; set; }
		public int Score { get; set; }
		public DateTime DateUTC { get; set; }

		public HospitalScore()
		{

		}

		public HospitalScore(Guid hospitalId, string user, int score)
		{
			Id = new Key(hospitalId, user);
			Score = score;
			DateUTC = DateTime.UtcNow;
		}
	}
}
