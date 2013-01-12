
using System;
using Nancy;
using NUnit.Framework;
using Photon.Web.Models;
using Photon.Web.Security;

namespace Photon.Specifications.SecurityTests
{
	[TestFixture]
	[Category("Security")]
	public class PhotonUserMapperTests:TestBaseWithDatabase
	{
		[Test]
		public void GetUserFromIdentifier_Returns_Valid_UserIdentity()
		{
			var guid = Guid.NewGuid();
				
			using(var session = this.GetNewSession())
			{
				var photonUser = new PhotonUser
				{
					ExternalId = guid,
					UserName = "chandu"
				};
				session.Store(photonUser);
				session.SaveChanges();
				var userMapper = new PhotonUserMapper(session);
				var fakeContext = new NancyContext();
				var userIdentity = userMapper.GetUserFromIdentifier(guid, fakeContext);
				Assert.NotNull(userIdentity);
				Assert.AreEqual("chandu", userIdentity.UserName);
			}
			
		}
	}
}
