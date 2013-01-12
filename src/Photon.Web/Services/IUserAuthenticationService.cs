
using System;

namespace Photon.Web.Services
{
	public interface IUserAuthenticationService
	{
		bool ValidateUser(String UserName, String Password);
	}
}
