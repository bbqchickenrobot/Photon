
using System;
using System.Linq;
using Photon.Web.Models;
using Raven.Client;
using Raven.Client.Linq;

namespace Photon.Web.Services
{
	public class PhotonUserService : PhotonServiceBase<PhotonUser>, IPhotonUserService
	{
		public PhotonUserService(IDocumentSession session)
			: base(session)
		{
		}
		public virtual PhotonUser GetUserByUserName(String userName)
		{
			return this.All()
				.Where(a => a.UserName == userName)
				.FirstOrDefault();
		}
	}
}
