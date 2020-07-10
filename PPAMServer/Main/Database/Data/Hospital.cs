using LiteDB;
using System;
namespace PPAMServer.Database.Data
{
	class Hospital
	{
		public class Position
		{
			public double Lat { get; set; }
			public double Lng { get; set; }
		}

		[BsonId]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Position Location { get; set; }

		public Hospital()
		{

		}
		public Hospital(string name, string description, Position location)
		{
			Id = Guid.NewGuid();
			Name = name;
			Description = description;
			Location = location;
		}
		public Hospital(Hospital hospital)
		{
			Id = hospital.Id;
			Name = hospital.Name;
			Description = hospital.Description;
			Location = hospital.Location;
		}
	}
}
