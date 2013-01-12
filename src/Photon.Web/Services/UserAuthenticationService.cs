
using System;
using System.Linq;
using Photon.Web.Extensions;
using Photon.Web.Models;
using Raven.Client;
using Raven.Client.Linq;

namespace Photon.Web.Services
{
	public class UserAuthenticationService: IUserAuthenticationService
	{
		protected virtual IDocumentSession Session {get; set;}
		public UserAuthenticationService(IDocumentSession session)
		{
			this.Session = session;
		}
		
		public virtual bool ValidateUser(String UserName, String Password)
		{
			var user = this.Session.Query<PhotonUser>().Where(a => a.UserName == UserName).FirstOrDefault();
			if(user != null)
			{
				//TODO: Change this to a better check using more secure methods
				return user.Password == Password.ToMD5Hash();
			}
			return false;
		}
	}
}
