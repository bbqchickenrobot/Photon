using System;
using Nancy;
using Nancy.Testing;
using Nancy.Diagnostics.Modules;
using NUnit.Framework;
using Photon.Web.Core;

namespace Photon.Specifications.IntegrationTests.ModuleSpecs
{
	public class MainModuleSpecs
	{
		[TestFixture]
		public class When_default_url_is_called
		{
			[Ignore]
			public void should_return_home_view()
			{
				// Given
				var bootstrapper = new PhotonTestBootStrapper();
				var browser = new Browser(bootstrapper);
				 
				// When
				var result = browser.Get("/", with => {
				    with.HttpRequest();
				});
				    
				// Then
				Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
			}
		}
		
		[TestFixture]
		public class When_recent_albums_is_called
		{
			[Ignore]
			public void should_return_a_list_of_recent_albums()
			{
				// Given
				var bootstrapper = new PhotonTestBootStrapper();
				var browser = new Browser(bootstrapper);
				 
				// When
				var result = browser.Get("/recent-albums", with => {
				    with.HttpRequest();
				});
				    
				// Then
				Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
			}
		}
		
	}
}
