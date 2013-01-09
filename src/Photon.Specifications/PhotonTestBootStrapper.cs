
using System;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Photon.Web.Security;
using Raven.Client;
using Raven.Client.Embedded;

namespace Photon.Specifications
{
	public class PhotonTestBootstrapper:DefaultNancyBootstrapper
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
		
		protected override void RequestStartup(TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines, NancyContext context)
		{
			base.RequestStartup(container, pipelines, context);
			var formsAuthConfiguration =
	            new FormsAuthenticationConfiguration()
	            {
	                RedirectUrl = "~/login",
	                UserMapper = container.Resolve<IUserMapper>(),
	            };
	
	        FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
		}

		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
		{
			base.ConfigureRequestContainer(container, context);
			container.Register<IUserMapper, PhotonUserMapper>();
			container.Register<IDocumentSession>((c, p) => {
				return 
					c.Resolve<IDocumentStore>()
					.OpenSession();
			});
		}
		
		
	}
}
