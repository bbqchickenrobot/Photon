
using System;
using NUnit.Framework;
using Photon.Web.Models;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Raven.Client.Listeners;
using Raven.Database.Server;

namespace Photon.Specifications
{
	public class NoStaleQueriesListener : IDocumentQueryListener
	{
	    #region Implementation of IDocumentQueryListener
	
	    public void BeforeQueryExecuted(IDocumentQueryCustomization queryCustomization)
	    {
	        queryCustomization.WaitForNonStaleResults();
	    }
	
	    #endregion
	}
		
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
			IndexCreation.CreateIndexes(typeof(Album).Assembly, this.DocumentStore);
			//this.DocumentStore.RegisterListener(new NoStaleQueriesListener());
			
		}
		
		protected IDocumentSession GetNewSession()
		{
			return this.DocumentStore.OpenSession();
		}
			
	}
}
