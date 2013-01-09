using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photon.Web.Security
{
	public class PhotonUserIdentity:IUserIdentity
	{
		public IEnumerable<string> Claims
		{
			get; set;
		}

		public string UserName
		{
			get; set;
		}
	}
}