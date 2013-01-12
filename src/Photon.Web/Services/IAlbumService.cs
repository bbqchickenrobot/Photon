
using System;
using System.Collections.Generic;
using Photon.Web.Models;

namespace Photon.Web.Services
{
	public interface IAlbumService
	{
		Album Load(string id);
		Album Save(Album album);
		void Delete(Album album);
		
		IList<Album> Recent(int limit);
		IList<Album> FindByTags(IList<String> tags);
	}
}
