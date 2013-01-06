﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Conventions;

using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Nancy.TinyIoc;

namespace Photon.Web.Core
{
	public class PhotonBootstrapper:DefaultNancyBootstrapper
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
				DataDirectory = "PhotonDB"
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
		
		protected override Nancy.Diagnostics.DiagnosticsConfiguration DiagnosticsConfiguration
		{
			get { return new Nancy.Diagnostics.DiagnosticsConfiguration { Password = "password" }; }
		}
	}
}