
using System;
using NUnit.Framework;
using Photon.Web.Extensions;
using Photon.Web.Models;
using Photon.Web.Services;

namespace Photon.Specifications.ServicesTests
{
	[TestFixture]
	public class UserAuthenticationServiceTests:TestBaseWithDatabase
	{
		public UserAuthenticationServiceTests()
		{
		}
		
		[Test]
		public void ValidateUser_Returns_True_For_Valid_UserName_Password()
		{
			using(var session = this.GetNewSession())
			{
				var userName = "SomeUser";
				var password = "Secret";
				var photonUser = new PhotonUser
				{
					UserName = "SomeUser",
					Password = password.ToMD5Hash()
				};
				session.Store(photonUser);
				session.SaveChanges();
				
				var userAuthenticationService = new UserAuthenticationService(session);
				
				var validationResult = userAuthenticationService.ValidateUser(userName, password);
				Assert.IsTrue(validationResult);
			}
			
		}
	}
}
