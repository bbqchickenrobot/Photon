using System;
using NUnit;
using NUnit.Framework;
using Photon.Web.Models;
using Photon.Web.Services;
using Raven.Client;
using Raven.Client.Embedded;

namespace Photon.Specifications.IntegrationTests.ServiceSpecs
{
	[TestFixture]
	public class AlbumServiceSpecs
	{
		[TestFixture]
		public class When_calling_Recent :TestBaseWithDatabase
		{
			
			[Test]
			public void should_return_empty_list_if_no_albums_exists()
			{
				//Arrange
				var session = this.GetNewSession();
				var albumService = new AlbumService(session);
				
				//Act
				var albums = albumService.Recent(10);
				//Assert
				Assert.AreEqual(0, albums.Count);
				
			}
			
			[Test]
			public void should_return_latest_3_albums_when_limit_3_is_specified()
			{
				//Arrange
				var session = this.GetNewSession();
				session.Store(new Album());
				session.Store(new Album());
				session.Store(new Album());
				session.SaveChanges();
				var albumService = new AlbumService(session);
				
				//Act
				var albums = albumService.Recent(3);
				//Assert
				Assert.AreEqual(3, albums.Count);
			}		
		}
		
		[TestFixture]
		public class When_calliing_AddAlbum
		{
			
			
		}
			
	}
}
