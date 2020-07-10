using LiteDB;
using System;

namespace PPAMServer.Database.Data
{
	class HospitalComment
	{
		[BsonId]
		public Guid Id { get; set; }

		public Guid HospitalId { get; set; }
		public string User { get; set; }

		public string Comment { get; set; }
		public DateTime DateUTC { get; set; }

		public HospitalComment()
		{

		}
		public HospitalComment(Guid hospitalId, string user, string comment)
		{
			Id = Guid.NewGuid();
			HospitalId = hospitalId;
			User = user;
			Comment = comment;
			DateUTC = DateTime.UtcNow;
		}
	}
}
