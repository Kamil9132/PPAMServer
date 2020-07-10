using LiteDB;
using Microsoft.CSharp.RuntimeBinder;
using System.Collections.Generic;
using System.Reflection;

namespace Core.LiteDb
{
	abstract class Db
	{
		public LiteDatabase LiteDatabase { get; }

		private void DropNotUsedCollections()
		{
			var usedCollections = new HashSet<string>();
			var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			var collections = LiteDatabase.GetCollectionNames();

			foreach (var property in properties)
			{
				dynamic databaseCollection = property.GetValue(this);

				try
				{
					usedCollections.Add(databaseCollection.Name);
				}
				catch (RuntimeBinderException)
				{

				}
			}

			foreach (var collectionName in collections)
			{
				if (!usedCollections.Contains(collectionName))
				{
					LiteDatabase.DropCollection(collectionName);
				}
			}
		}

		protected abstract string GetDatabaseName();

		protected abstract void CreateCollections();

		public Db(string path)
		{
			var fileName = path + GetDatabaseName();

			LiteDatabase = new LiteDatabase($"filename={fileName};utc=true");

			CreateCollections();
			DropNotUsedCollections();
		}
	}
}
