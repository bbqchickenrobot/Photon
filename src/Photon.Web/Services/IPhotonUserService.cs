
using Photon.Web.Models;
using System;

namespace Photon.Web.Services
{
	public interface IPhotonUserService
	{
			PhotonUser Load(string id);
			PhotonUser Save(PhotonUser photonUser);
			void Delete(PhotonUser photonUser);
			PhotonUser GetUserByUserName(String userName);
	}
}
