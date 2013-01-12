using System;
using NUnit.Framework;
using Photon.Web.Models;
using Photon.Web.Services;

namespace Photon.Specifications.ServicesTests
{
	[TestFixture]
	public class PhotonUserServiceTests:TestBaseWithDatabase
	{
		[Test]
		public void GetUserByUserName_Returns_PhotonUser_For_Existing_UserName()
		{
			using(var session = this.GetNewSession())
			{
				var photonUser = new PhotonUser
				{
					UserName = "SomeUser"
				};
				session.Store(photonUser);
				session.SaveChanges();
				
				var userService = new PhotonUserService(session);
				
				var user = userService.GetUserByUserName(photonUser.UserName);
				Assert.NotNull(user);
				Assert.AreEqual(photonUser.UserName, user.UserName);
			}
		}
	}
}
