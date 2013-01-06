
using System;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace Photon.Specifications
{
	public abstract class TestBaseWithDatabase
	{
		internal  IDocumentStore DocumentStore  {get; set;}
		[SetUp]
		public void SetUp()
		{
			this.DocumentStore = new EmbeddableDocumentStore
			{
				RunInMemory = true,
			};
			this.DocumentStore.Conventions.DefaultQueryingConsistency = ConsistencyOptions.QueryYourWrites;
			this.DocumentStore.Initialize();
		}
		
		protected IDocumentSession GetNewSession()
		{
			return this.DocumentStore.OpenSession();
		}
			
	}
}
