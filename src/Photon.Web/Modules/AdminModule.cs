using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photon.Web.Modules
{
	public class AdminModule : NancyModule
	{
		public AdminModule()
			: base("admin")
		{
			Get["/"] = parameters =>
			{
				return View["AdminHome"];
			};
		}
	}
}