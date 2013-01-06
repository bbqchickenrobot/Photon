
using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Web.Extensions;
using Photon.Web.Models;
using Photon.Web.Services.Exceptions;
using Raven.Client;
using Raven.Client.Linq;

namespace Photon.Web.Services
{
	public class AlbumService:IAlbumService
	{
		private IDocumentSession Session {get; set;}
		public AlbumService(IDocumentSession session)
		{
			this.Session = session;
		}
		
		public virtual Album Load(string id)
		{
			return this.Session.Load<Album>(id);
		}
		
		public virtual IList<Album> Recent(int limit)
		{
			return this.Session
				.Query<Album>()
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
		
		public virtual Album Save(Album album)
		{
			
			var duplicateAlbumExists = this.IsDuplicateAlbum(album);
				
			if(duplicateAlbumExists)
			{
				throw new DuplicateEntityException("Album with name {0} already exists".FormatWith(album.Name));
			}
			this.Session.Store(album);
			this.Session.SaveChanges();
			return album;
		}
		
		public virtual void Delete(Album album)
		{
			this.Session.Delete(album);
			this.Session.SaveChanges();
		}
	}
}
