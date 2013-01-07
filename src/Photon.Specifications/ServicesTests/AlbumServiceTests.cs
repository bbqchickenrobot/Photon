using System;
using System.Linq;
using Nancy.Session;
using NUnit.Framework;
using Photon.Web.Models;
using Photon.Web.Services;
using Photon.Web.Services.Exceptions;

namespace Photon.Specifications.ServicesTests
{
	[TestFixture]
	public class AlbumServiceTests:TestBaseWithDatabase
	{
		[Test]
		public void Recent_Returns_SpecifiedLimit_Albums_When_There_Are_SpecifiedLimit_Albums()
		{
			var session = this.GetNewSession();
			
			Enumerable.Range(1, 5)
				.ToList()
				.ForEach(a => session.Store(new Album()));
			session.SaveChanges();
			var albumService = new AlbumService(session);
			var albums = albumService.Recent(5);
			
			Assert.AreEqual(5, albums.Count());
		}
		
		[Test]
		public void Recent_Returns_SpecifiedLimit_Albums_When_There_Are_MoreThan_SpecifiedLimit_Albums()
		{
			var session = this.GetNewSession();
			
			Enumerable.Range(1, 6)
				.ToList()
				.ForEach(a => session.Store(new Album()));
			session.SaveChanges();
			var albumService = new AlbumService(session);
			var albums = albumService.Recent(5);
			
			Assert.AreEqual(5, albums.Count());
		}
		
		[Test]
		public void Recent_Returns_0_Albums_When_There_Are_No_Albums()
		{
			var session = this.GetNewSession();
			
			var albumService = new AlbumService(session);
			var albums = albumService.Recent(5);
			
			Assert.AreEqual(0, albums.Count());
		}
		
		[Test]
		public void Save_PersistsNewAlbumToRepositorySuccesfully()
		{
			Album album = null;
			using(var session = this.GetNewSession())
			{
				var albumService = new AlbumService(session);	
				album = albumService.Save(new Album
				                              {
				                              	Name = "SomeAlbum"
				                              });
				Assert.NotNull(album);
				Assert.NotNull(album.Id);
				
				var savedAlbum = 
					session
					.Query<Album>()
					.Where(a => a.Name == album.Name)
					.FirstOrDefault();
				Assert.NotNull(savedAlbum);
				Assert.AreEqual(album.Id, savedAlbum.Id);
			}
			
		}
		
		[Test]
		public void Save_ThrowsException_For_Duplicate_Album_Name()
		{
			var session = this.GetNewSession();
			var firstAlbum = new  Album
			{
				Name = "Some Album"
			};
			session.Store(firstAlbum);
			session.SaveChanges();
				
			var albumService = new AlbumService(session);
			var duplicateAlbum = new Album
			{
				Name = "Some Album"
			};
			
			var ex = Assert.Throws<DuplicateEntityException>( () => albumService.Save(duplicateAlbum));
		}
		
		[Test]
		public void Save_ThrowsException_For_Duplicate_Album_Name_And_Different_Case()
		{
			var session = this.GetNewSession();
			var firstAlbum = new  Album
			{
				Name = "Some Album"
			};
			session.Store(firstAlbum);
			session.SaveChanges();
				
			var albumService = new AlbumService(session);
			var duplicateAlbum = new Album
			{
				Name = "some album"
			};
			
			var ex = Assert.Throws<DuplicateEntityException>( () => albumService.Save(duplicateAlbum));
		}
		
		[Test]
		public void Save_PersistsExistingAlbumToRepositorySuccesfully()
		{
			using(var session = this.GetNewSession())
			{
				var album = new Album
				{
					Name = "Some Stupid Album"
				};
				
				session.Store(album);
				var albumId = album.Id;
				
				var albumService = new AlbumService(session);
				album.Name = "Some Good Album";
				albumService.Save(album);
				
				Assert.AreEqual(albumId, album.Id);
				Assert.AreEqual("Some Good Album", album.Name);
			}
		}
		
		[Test]
		public void Delete_RemovesAlbumFromRepository()
		{
			using(var session = this.GetNewSession())
			{
				var album = new Album
				              {
				              	Name = "Some Album"
				              };
				session.Store(album);
				session.SaveChanges();
				var albumService = new AlbumService(session);
				albumService.Delete(album);
				var dummyAlbum = session.Load<Album>(album.Id);
				Assert.Null(dummyAlbum);
			}
		
		}
		
		[Test]
		public void Load_Returns_Album_For_Given_Id()
		{
			using(var session = this.GetNewSession())
			{
				var album = new Album
				{
					Name = "Some Album"
				};
				
				session.Store(album);
				session.SaveChanges();
				var albumService = new AlbumService(session);
				var queriedAlbum = albumService.Load(album.Id);
				Assert.NotNull(queriedAlbum.Id);
				Assert.AreEqual(album.Name, 	queriedAlbum.Name);
				
			}
		}
		
		[Test]
		public void Load_Returns_Null_For_NonExistent_Id()
		{
			using(var session = this.GetNewSession())
			{
				var albumService = new AlbumService(session);
				var queriedAlbum = albumService.Load("albums/1");
				Assert.Null(queriedAlbum);
				
			}
		}
		
		[Test]
		public void LoadForPhoto_Returns_Album_For_Existing_Photo()
		{
			using(var session = this.GetNewSession())
			{
				var album = new Album
				{
					Name = "Some Photo"
				};
				
				var photo = new Photo
				{
					Name = "Some Photo",
					Album = album
				};
				
				album.Photos.Add(photo);
				session.Store(album);
				session.SaveChanges();
				
				var albumService = new AlbumService(session);
				var queriedAlbum = albumService.LoadForPhoto(photo.Id);
				Assert.NotNull(queriedAlbum);
			}
		}
		
		[Test]
		public void Save_Persists_AddedPhotos_Of_Album_To_Repository()
		{
			using(var session = this.GetNewSession())
			{
				var album = new Album
				{
					Name = "Some Photo"
				};
				
				var photo = new Photo
				{
					Name = "Some Photo",
					Album = album
				};
				
				album.Photos.Add(photo);
				
				var albumService = new AlbumService(session);
				albumService.Save(album);
				var queriedAlbum = albumService.Load(album.Id);
				Assert.NotNull(queriedAlbum);
				Assert.AreEqual(1, queriedAlbum.Photos.Count());
			}
		}
		
		[Test]
		public void Save_Deletes_RemovedPhotos_Of_Album_From_Repository()
		{
			using(var session = this.GetNewSession())
			{
				var album = new Album
				{
					Name = "Some Photo"
				};
				
				var photo = new Photo
				{
					Name = "Some Photo",
					Album = album
				};
				
				album.Photos.Add(photo);
				session.Store(album);
				session.SaveChanges();
				
				album.Photos.Clear();
				var albumService = new AlbumService(session);
				albumService.Save(album);
				var queriedAlbum = albumService.Load(album.Id);
				Assert.NotNull(queriedAlbum);
				Assert.AreEqual(0, queriedAlbum.Photos.Count());
			}
		}
		
		[Test]
		public void Save_Persists_ChangedPhotos_Of_Album_To_Repository()
		{
			using(var session = this.GetNewSession())
			{
				var album = new Album
				{
					Name = "Some Photo"
				};
				
				var photo = new Photo
				{
					Name = "Some Photo",
					Album = album
				};
				
				album.Photos.Add(photo);
				session.Store(album);
				session.SaveChanges();
				
				photo.Name = "Changed Photo";
				
				var albumService = new AlbumService(session);
				albumService.Save(album);
				var queriedAlbum = albumService.Load(album.Id);
				Assert.NotNull(queriedAlbum);
				Assert.AreEqual("Changed Photo", queriedAlbum.Photos[0].Name);
			}
		}
		
		[Test]
		public void FindByTags_Returns_Albums_With_Macthing_Tags()
		{
			using(var session = this.GetNewSession())
			{
				var firstAlbum = new Album
				{
					Name = "First Album",
					Tags = new System.Collections.Generic.List<String>
					{
						"some", "any", "none"
					}
				};
				
				var secondAlbum = new Album
				{
					Name = "Second Album",
					Tags = new System.Collections.Generic.List<String>
					{
						"some"
					}
				};
				session.Store(firstAlbum);
				session.Store(secondAlbum);
				session.SaveChanges();
				
				var tags =new System.Collections.Generic.List<String>
					{
						"Some"
					};
				var albumService = new AlbumService(session);
				var albums = albumService.FindByTags(tags);
				
				Assert.AreEqual(2, albums.Count());
			}
		}
		
		[Test]
		public void FindPhotosByTags_Returns_Photos_With_Macthing_Tags()
		{
			using(var session = this.GetNewSession())
			{
				var album = new Album
				{
					
				};
				
				var firstPhoto= new Photo
				{
					Name = "First Photo",
					Tags = new System.Collections.Generic.List<String>
					{
						"some", "any", "none"
					}
				};
				
				var secondPhoto = new Photo
				{
					Name = "Second Photo",
					Tags = new System.Collections.Generic.List<String>
					{
						"some"
					}
				};
				
				album.Photos.Add(firstPhoto);
				album.Photos.Add(secondPhoto);
				session.Store(album);
				
				session.SaveChanges();
				
				var tags =new System.Collections.Generic.List<String>
					{
						"Some"
					};
				var albumService = new AlbumService(session);
				var photos = albumService.FindPhotosByTags(tags);
				
				Assert.AreEqual(2, photos.Count());
			}
		}
		
	}
}
