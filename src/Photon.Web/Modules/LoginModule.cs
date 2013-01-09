
using System;
using Nancy;

namespace Photon.Web.Modules
{
	public class LoginModule:NancyModule
	{
		public LoginModule():base("login")
		{
			Get["/"] = parameters => {
					return "Login Page";
			};
		}
	}
}
