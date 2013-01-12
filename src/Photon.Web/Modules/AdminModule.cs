using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Security;

namespace Photon.Web.Modules
{
	public class AdminModule : NancyModule
	{
		public AdminModule()
			: base("/admin")
		{
			this.RequiresAuthentication();
			Get["/"] = parameters =>
			{
				return View["AdminHome"];
			};
		}
	}
}