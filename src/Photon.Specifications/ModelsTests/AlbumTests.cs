
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Photon.Web.Models;

namespace Photon.Specifications.ModelsTests
{
	[TestFixture]
	public class AlbumTests
	{
		
		[Test]
		public void VisibilityType_Returns_Public_If_No_Photos_Exist()
		{
			var album = new Album
			{
				Name = "Some Album"
			};
			Assert.AreEqual(VisibilityType.Public, album.VisibilityType);
		}
	}
}
