using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Conventions;
using Nancy.Bootstrappers.StructureMap;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace Photon.Web.Core
{
	public class PhotonBootstrapper:StructureMapNancyBootstrapper
	{
		protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
		{
			base.ConfigureConventions(nancyConventions);
			nancyConventions.StaticContentsConventions.Add(
						StaticContentConventionBuilder.AddDirectory("assets", @"assets")
				);
		}
		
		protected override void ApplicationStartup(StructureMap.IContainer container, Nancy.Bootstrapper.IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);
		}
		
		protected override void ConfigureApplicationContainer(StructureMap.IContainer existingContainer)
		{
			base.ConfigureApplicationContainer(existingContainer);
			
			var docStore = new EmbeddableDocumentStore
			{
				DataDirectory = "PhotonDB"
			};
			docStore.Initialize();
			existingContainer.Configure(config=> 
			                            {
			                            	config
			                            		.For<IDocumentStore>()
			                            		.Singleton()
			                            		.Use(docStore)
			                            		;
			                            });
		}
		
		
		protected override void ConfigureRequestContainer(StructureMap.IContainer container, NancyContext context)
		{
			base.ConfigureRequestContainer(container, context);
			container.Configure(config => 
			                    {
			                    	config
			                    		.For<IDocumentSession>()
			                    		.Use(() => container.GetInstance<IDocumentStore>().OpenSession());
			                    });
		}
		
		protected override Nancy.Diagnostics.DiagnosticsConfiguration DiagnosticsConfiguration
		{
			get { return new Nancy.Diagnostics.DiagnosticsConfiguration { Password = "password" }; }
		}
	}
}