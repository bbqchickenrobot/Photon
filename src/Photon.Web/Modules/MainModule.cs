using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photon.Web.Modules
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = parameters =>
			{
				return View["Home"];
			};
		}
	}
}