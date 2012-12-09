
using System;
using System.Collections.Generic;
using Photon.Web.Models;

namespace Photon.Web.Services
{
	public interface IAlbumService
	{
		IList<Album> RecentAlbums(int limit);
	}
}
