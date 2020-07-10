using Core.Network.Http.Native;
using Core.Network.Http.Native.Manager;
using Core.Operations;
using LiteDB;
using Newtonsoft.Json;
using PPAMServer.Database;
using PPAMServer.Managers.Data;
using System;
using System.Collections.Generic;

namespace PPAMServer.Managers
{
	class Manager : BaseManager
	{
		private static readonly string developerKey = "wGKXKLf0bEUa7V8M4UC0olJymZ4p888h";
		private static readonly Response errorResponse = new Response("Bad Request");

		private readonly object dbLockObject = new object();

		private readonly Db db;

		private Database.Data.Hospital GetDatabaseHospital(Dictionary<string, string> pathValues)
		{
			if (!Guid.TryParse(pathValues["id"], out var id))
			{
				return null;
			}

			return db.Hospitals.FindById(id);
		}

		private Hospital GetHospitalData(Database.Data.Hospital hospital)
		{
			if (hospital == null)
			{
				return null;
			}

			var scores = db.HospitalScores.Find(item => item.Id.HospitalId == hospital.Id);
			var comments = db.HospitalComments.Find(item => item.HospitalId == hospital.Id);

			return new Hospital(hospital, scores, comments);
		}

		[RequestHandler("/hospital", RequestHeaders.MethodType.Get)]
		private object OnGetHospitalsRequest(RequestParameters requestParameters, Dictionary<string, string> pathValues)
		{
			return db.Hospitals.FindAll();
		}
		[RequestHandler("/hospital/{id}", RequestHeaders.MethodType.Get)]
		private object OnGetHospitalRequest(RequestParameters requestParameters, Dictionary<string, string> pathValues)
		{
			var hospital = GetDatabaseHospital(pathValues);

			if (hospital == null)
			{
				return errorResponse;
			}

			return GetHospitalData(hospital);
		}

		[RequestHandler("/hospital/{id}/score", RequestHeaders.MethodType.Post)]
		private object OnCreateHospitalScoreRequest(RequestParameters requestParameters, Dictionary<string, string> pathValues)
		{
			var postData = requestParameters.PostData.Dictionary;
			var hospital = GetDatabaseHospital(pathValues);

			if (hospital == null || 
				!StandardDataOperations.AreKeysInDictionary(postData, new string[] { "user", "score" }) ||
				!int.TryParse(postData["score"], out var score))
			{
				return errorResponse;
			}

			var user = postData["user"];

			if (string.IsNullOrEmpty(user))
			{
				return errorResponse;
			}

			var databaseScore = new Database.Data.HospitalScore(hospital.Id, user, score);

			lock (dbLockObject)
			{
				db.HospitalScores.Upsert(databaseScore);
			}

			return GetHospitalData(hospital);
		}

		[RequestHandler("/hospital/{id}/comment", RequestHeaders.MethodType.Post)]
		private object OnCreateHospitalCommentRequest(RequestParameters requestParameters, Dictionary<string, string> pathValues)
		{
			var postData = requestParameters.PostData.Dictionary;
			var hospital = GetDatabaseHospital(pathValues);

			if (hospital == null || !StandardDataOperations.AreKeysInDictionary(postData, new string[] { "user", "comment" }))
			{
				return errorResponse;
			}

			var user = postData["user"];
			var comment = postData["comment"];

			if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(comment))
			{
				return errorResponse;
			}

			var databaseComment = new Database.Data.HospitalComment(hospital.Id, user, comment);

			lock (dbLockObject)
			{
				db.HospitalComments.Upsert(databaseComment);
			}

			return GetHospitalData(hospital);
		}

		[RequestHandler("/developer/replace-data", RequestHeaders.MethodType.Post)]
		private object OnDeveloperReplaceDataRequest(RequestParameters requestParameters, Dictionary<string, string> pathValues)
		{
			var postData = requestParameters.PostData.ObjectDictionary;

			if (!StandardDataOperations.AreKeysInDictionary(postData, new string[] { "developerKey", "data" }))
			{
				return errorResponse;
			}

			if (postData["developerKey"].ToString() != developerKey)
			{
				return errorResponse;
			}

			Database.Data.Hospital[] hospitals;

			try
			{
				hospitals = JsonOperations.DeserializeObjectWithCameCase<Database.Data.Hospital[]>(postData["data"].ToString());
			}
			catch (JsonException)
			{
				hospitals = null;
			}

			if (hospitals == null)
			{
				return errorResponse;
			}

			lock (dbLockObject)
			{
				db.Hospitals.Delete(Query.All());
				db.HospitalScores.Delete(Query.All());
				db.HospitalComments.Delete(Query.All());

				foreach (var hospital in hospitals)
				{
					hospital.Id = Guid.NewGuid();

					db.Hospitals.Insert(hospital);
				}
			}

			return db.Hospitals.FindAll();
		}

		protected override string GetSupportedPathPrefix()
		{
			return "/api";
		}

		public Manager(Db db)
		{
			this.db = db;
		}
	}
}
