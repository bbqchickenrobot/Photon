using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photon.Web.ViewModels
{
	public class LoginViewModel
	{
		public String UserName { get; set; }
		public String Password { get; set; }
		public bool RememberMe { get; set; }
		public String ErrorMessage { get; set; }
	}
}