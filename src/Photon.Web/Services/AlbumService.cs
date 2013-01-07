
using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Web.Extensions;
using Photon.Web.Models;
using Photon.Web.Services.Exceptions;
using Raven.Client;
using Raven.Client.Linq;
using Raven.Database.Linq.PrivateExtensions;

namespace Photon.Web.Services
{
	public class AlbumService: PhotonServiceBase<Album>, IAlbumService
	{
		public AlbumService(IDocumentSession session):base(session)
		{
		}
		
		
		public virtual IList<Album> Recent(int limit)
		{
			return 
				this.All()
				.OrderBy(a => a.ModifiedDate)
				.Take(limit)
				.ToList();
		}
		
		public virtual bool IsDuplicateAlbum(Album album)
		{
			return  this.Session
				.Query<Album>()
				.Any(a => a.Name == album.Name && a.Id != album.Id);
		}
		
		public override Album Save(Album album)
		{
			var duplicateAlbumExists = this.IsDuplicateAlbum(album);
			if(duplicateAlbumExists)
			{
				throw new DuplicateEntityException("Album with name {0} already exists".FormatWith(album.Name));
			}
			return base.Save(album);
		}
		
		public virtual Album LoadForPhoto(string photoId)
		{
			return this.All().Where(a => a.Photos.Any(p => p.Id == photoId)).FirstOrDefault();
		}
		
		public virtual IList<Album> FindByTags(IList<String> tags)
		{
			return this.All()
				.Where(a => a.Tags.Any(b => b.In(tags)))
				.ToList();
		}
		
		public IList<Photo> FindPhotosByTags(IList<String> tags)
		{
			var results = 
				from a in this.All()
				from p in a.Photos
				where p.Tags.Any(b => b.In(tags))
				select p;
			return results.ToList();
			
		}
	}
}
