using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photon.Web.Models;
using Raven.Client;

namespace Photon.Web.Modules
{
	public class MainModule : NancyModule
	{
		public MainModule(IDocumentSession DocSession)
		{
			Get["/"] = parameters =>
			{
				return View["Home"];
			};
			
			Get["/recent-albums"] = parameters =>
			{
				var albums = DocSession.Query<Album>();
				return Response.AsJson(albums);
			};
		}
	}
}