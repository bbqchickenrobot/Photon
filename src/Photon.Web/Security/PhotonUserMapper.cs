
using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using Photon.Web.Models;
using Raven.Client;

namespace Photon.Web.Security
{
	public class PhotonUserMapper:IUserMapper
	{
		protected IDocumentSession Session {get; set;}
		public PhotonUserMapper (IDocumentSession session)
		{
			this.Session = session;
		}
		
        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
	        		var userRecord = this.Session.Query<PhotonUser>().Where(u => u.ExternalId == identifier).FirstOrDefault();
	
	            	return userRecord == null
	                       ? null
	                       : new PhotonUserIdentity {UserName = userRecord.UserName};
	        	
        }
      
	}
}
