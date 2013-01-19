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
	public class PhotoServiceTests:TestBaseWithDatabase
	{
		[Test]
		public void Recent_Returns_SpecifiedLimit_Photos_When_There_Are_SpecifiedLimit_Photos()
		{
			var session = this.GetNewSession();
			
			Enumerable.Range(1, 5)
				.ToList()
				.ForEach(a => session.Store(new Photo()));
			session.SaveChanges();
			var photoService = new PhotoService(session);
			var photos = photoService.Recent(5);
			
			Assert.AreEqual(5, photos.Count());
		}
		
		[Test]
		public void Recent_Returns_SpecifiedLimit_Photos_When_There_Are_MoreThan_SpecifiedLimit_Photos()
		{
			var session = this.GetNewSession();
			
			Enumerable.Range(1, 6)
				.ToList()
				.ForEach(a => session.Store(new Photo()));
			session.SaveChanges();
			var photoService = new PhotoService(session);
			var photos = photoService.Recent(5);
			
			Assert.AreEqual(5, photos.Count());
		}
		
		[Test]
		public void Recent_Returns_0_Photos_When_There_Are_No_Photos()
		{
			var session = this.GetNewSession();
			
			var photoService = new PhotoService(session);
			var photos = photoService.Recent(5);
			
			Assert.AreEqual(0, photos.Count());
		}
		
		[Test]
		public void Save_PersistsNewPhotoToRepositorySuccesfully()
		{
			Photo photo = null;
			using(var session = this.GetNewSession())
			{
				var photoService = new PhotoService(session);	
				photo = photoService.Save(new Photo
				                              {
				                              	Name = "SomePhoto"
				                              });
				Assert.NotNull(photo);
				Assert.NotNull(photo.Id);
				
				var savedPhoto = 
					session
					.Query<Photo>()
					.Where(a => a.Name == photo.Name)
					.FirstOrDefault();
				Assert.NotNull(savedPhoto);
				Assert.AreEqual(photo.Id, savedPhoto.Id);
			}
			
		}
		
		[Test]
		public void Save_ThrowsException_For_Duplicate_Photo_Name_Or_Photo_Path()
		{
			var session = this.GetNewSession();
			var firstPhoto = new  Photo
			{
				Name = "Some Photo",
				Path = "samepath"
			};
			session.Store(firstPhoto);
			session.SaveChanges();
				
			var photoService = new PhotoService(session);
			var duplicatePhoto = new Photo
			{
				Name = "Other Photo",
				Path = "samepath"
			};
			
			var ex = Assert.Throws<DuplicateEntityException>( () => photoService.Save(duplicatePhoto));
		}
		
		[Test]
		public void Save_ThrowsException_For_Duplicate_Photo_Name_And_Different_Case()
		{
			var session = this.GetNewSession();
			var firstPhoto = new  Photo
			{
				Name = "Some Photo"
			};
			session.Store(firstPhoto);
			session.SaveChanges();
				
			var photoService = new PhotoService(session);
			var duplicatePhoto = new Photo
			{
				Name = "some photo"
			};
			
			var ex = Assert.Throws<DuplicateEntityException>( () => photoService.Save(duplicatePhoto));
		}
		
		[Test]
		public void Save_ThrowsException_For_Duplicate_Photo_Path_And_Different_Case()
		{
			var session = this.GetNewSession();
			var firstPhoto = new  Photo
			{
				Path = "Some Photo"
			};
			session.Store(firstPhoto);
			session.SaveChanges();
				
			var photoService = new PhotoService(session);
			var duplicatePhoto = new Photo
			{
				Path = "some photo"
			};
			
			var ex = Assert.Throws<DuplicateEntityException>( () => photoService.Save(duplicatePhoto));
		}
		
		[Test]
		public void Save_Persists_ExistingPhoto_To_Repository_Succesfully()
		{
			using(var session = this.GetNewSession())
			{
				var photo = new Photo
				{
					Name = "Some Stupid Photo"
				};
				
				session.Store(photo);
				var photoId = photo.Id;
				
				var photoService = new PhotoService(session);
				photo.Name = "Some Good Photo";
				photoService.Save(photo);
				
				Assert.AreEqual(photoId, photo.Id);
				Assert.AreEqual("Some Good Photo", photo.Name);
			}
		}
		
		[Test]
		public void Delete_Removes_Photo_From_Repository()
		{
			using(var session = this.GetNewSession())
			{
				var photo = new Photo
				              {
				              	Name = "Some Photo"
				              };
				session.Store(photo);
				session.SaveChanges();
				var photoService = new PhotoService(session);
				photoService.Delete(photo.Id);
				var dummyPhoto = session.Load<Photo>(photo.Id);
				Assert.Null(dummyPhoto);
			}
		
		}
		
		[Test]
		public void Load_Returns_Photo_For_Given_Id()
		{
			using(var session = this.GetNewSession())
			{
				var photo = new Photo
				{
					Name = "Some Photo"
				};
				
				session.Store(photo);
				session.SaveChanges();
				var photoService = new PhotoService(session);
				var queriedPhoto = photoService.Load(photo.Id);
				Assert.NotNull(queriedPhoto.Id);
				Assert.AreEqual(photo.Name, 	queriedPhoto.Name);
				
			}
		}
		
		[Test]
		public void Load_Returns_Null_For_NonExistent_Id()
		{
			using(var session = this.GetNewSession())
			{
				var photoService = new PhotoService(session);
				var queriedPhoto = photoService.Load("photos/1");
				Assert.Null(queriedPhoto);
				
			}
		}
		
		[Test]
		public void FindByTags_Returns_Photos_With_Matching_Tags()
		{
			using(var session = this.GetNewSession())
			{
				var firstPhoto = new Photo
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
				session.Store(firstPhoto);
				session.Store(secondPhoto);
				session.SaveChanges();
				
				var tags =new System.Collections.Generic.List<String>
					{
						"Some"
					};
				var photoService = new PhotoService(session);
				var photos = photoService.FindByTags(tags);
				
				Assert.AreEqual(2, photos.Count());
			}
		}
	}
}
