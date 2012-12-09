
using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Web.Models;
using Raven.Client;
using Raven.Client.Linq;

namespace Photon.Web.Services
{
	public class AlbumService:IAlbumService
	{
		private IDocumentSession Session {get; set;}
		public AlbumService(IDocumentSession Session)
		{
			this.Session = Session;
		}
		
		public virtual IList<Album> RecentAlbums(int limit)
		{
			return this.Session
				.Query<Album>()
				.OrderBy(a => a.ModifiedDate)
				.Take(limit)
				.ToList();
		}
	}
}
