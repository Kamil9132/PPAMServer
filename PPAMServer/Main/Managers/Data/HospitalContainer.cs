using System.Collections.Generic;

namespace PPAMServer.Managers.Data
{
	class HospitalContainer
	{
		public IEnumerable<Database.Data.Hospital> Hospitals { get; set; }

		public HospitalContainer(IEnumerable<Database.Data.Hospital> hospitals)
		{
			Hospitals = hospitals;
		}
	}
}
