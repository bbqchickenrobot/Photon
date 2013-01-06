
using System;
using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Raven.Client;
using Raven.Client.Embedded;

namespace Photon.Specifications
{
	public class PhotonTestBootStrapper:DefaultNancyBootstrapper
	{
		protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
		{
			base.ConfigureConventions(nancyConventions);
			nancyConventions.StaticContentsConventions.Add(
						StaticContentConventionBuilder.AddDirectory("assets", @"assets")
			);
		}

		protected override void ConfigureApplicationContainer(TinyIoCContainer existingContainer)
		{
			base.ConfigureApplicationContainer(existingContainer);
			
			var docStore = new EmbeddableDocumentStore
			{
				RunInMemory = true
			};
			docStore.Initialize();

			existingContainer
				.Register<IDocumentStore>(docStore);
		}

		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
		{
			base.ConfigureRequestContainer(container, context);
			container.Register<IDocumentSession>((c, p) => {
				return 
					c.Resolve<IDocumentStore>()
					.OpenSession();
			});
		}
		
		
	}
}
