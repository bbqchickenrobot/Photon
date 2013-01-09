using System;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using Photon.Web.Core;

namespace Photon.Specifications.ModulesTests
{
	[TestFixture]
	[Category("Main Module")]
	public class MainModuleTests:TestBaseWithDatabase
	{
		public MainModuleTests()
		{
			
		}
		
		[Test]
		public void DefaultPath_ShouldReturn_HomeViewModel()
		{
			// Given
		    var bootstrapper = new PhotonTestBootstrapper();
		    var browser = new Browser(bootstrapper);
		     
		    // When
		    var result = browser.Get("/", with => {
		        with.HttpRequest();
		    });
		        
		    // Then
		    Assert.AreEqual (HttpStatusCode.OK, result.StatusCode);
		    
		}
	}
}
