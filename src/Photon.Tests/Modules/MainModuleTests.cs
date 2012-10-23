using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Photon.Web.Core;
using Nancy.Testing;
using Nancy;

namespace Photon.Tests.Modules
{
	[TestFixture]
	public class When_default_page_is_requested
	{
		[Test]
		public void Should_return_status_ok()
		{
			// Given
			var bootstrapper = new PhotonBootstrapper();
			var browser = new Browser(bootstrapper);

			// When
			var result = browser.Get("/", with =>
			{
				with.HttpRequest();
			});

			// Then
			Console.WriteLine(result.StatusCode);

			Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
		}
	}
}
