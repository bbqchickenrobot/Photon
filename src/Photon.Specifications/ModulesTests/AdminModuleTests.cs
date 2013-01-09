using System;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using Photon.Web.Core;

namespace Photon.Specifications.ModulesTests
{
	[TestFixture]
	public class AdminModuleTests:TestBaseWithDatabase
	{
		
		[Ignore("Ignoring it till I learn how to TDD NancyFx Forms Auth")]
		public void AdminModule_ShouldThrow_NotAuthenticated_If_User_Is_Not_LoggedIn()
		{
			// Given
			var bootstrapper = new PhotonTestBootstrapper();
		    var browser = new Browser(bootstrapper);
		     
		    // When
		    var result = browser.Get("/admin", with => {
		        with.HttpRequest();
		    });
		        
		    // Then
		    Assert.AreEqual (HttpStatusCode.Forbidden, result.StatusCode);
		}
	}
}
