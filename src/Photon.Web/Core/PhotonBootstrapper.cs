using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Conventions;

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
		protected override Nancy.Diagnostics.DiagnosticsConfiguration DiagnosticsConfiguration
		{
			get { return new Nancy.Diagnostics.DiagnosticsConfiguration { Password = "password" }; }
		}
	}
}