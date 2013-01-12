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
		[Test]
		public void AdminModule_Should_Redirect_To_Login_If_User_Is_Not_LoggedIn()
		{
			// Given
			var bootstrapper = new PhotonTestBootstrapper();
		    var browser = new Browser(bootstrapper);
		     
		    // When
		    var response = browser.Get("/admin", with => {
		        with.HttpRequest();
		    });
		        
		    // Then
		    response.ShouldHaveRedirectedTo("/login?returnUrl=/admin");
		    //Assert.AreEqual (HttpStatusCode.SeeOther, result.StatusCode);
		}
	}
}
