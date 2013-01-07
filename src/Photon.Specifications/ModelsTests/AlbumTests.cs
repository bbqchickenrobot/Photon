
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
		public void VisibilityType_Returns_Public_If_No_Photos_Are_Private()
		{
			var album = new Album
			{
				Name = "Some Album"
			};
			var photo = new Photo
			{
				Album = album,
				IsPrivate = false
			};
			
			album.Photos.Add(photo);
			Assert.AreEqual(VisibilityType.Public, album.VisibilityType);
		}
		
		[Test]
		public void VisibilityType_Returns_Public_If_No_Photos_Exist()
		{
			var album = new Album
			{
				Name = "Some Album"
			};
			Assert.AreEqual(VisibilityType.Public, album.VisibilityType);
		}
		
		[Test]
		public void VisibilityType_Returns_Private_If_All_Photos_Are_Private()
		{
			var album = new Album
			{
				Name = "Some Album"
			};
			
			album.Photos = new List<Photo>
			{
				new Photo{IsPrivate = true},
				new Photo{IsPrivate = true}
			};
			Assert.AreEqual(VisibilityType.Private, album.VisibilityType);
		}
		
		[Test]
		public void VisibilityType_Returns_Public_If_Atleast_One_Photo_Is_Public()
		{
			var album = new Album
			{
				Name = "Some Album"
			};
			
			album.Photos = new List<Photo>
			{
				new Photo{IsPrivate = true},
				new Photo{IsPrivate = true},
				new Photo{IsPrivate = false}
			};
			Assert.AreEqual(VisibilityType.Public, album.VisibilityType);
		}
	}
}
