
using System;
using Photon.Web.Models;
using Raven.Client.Indexes;

namespace Photon.Web.DataStore.Indexes
{
	public class PhotoTagsIndex:AbstractIndexCreationTask<Album>
	{
		public PhotoTagsIndex()
		{
			Map = albums =>
				from a in albums
				from p in a.Photos
				select new PhotoTags
			{
				PhotoId = p.Id,
				Tags = p.Tags
			};
		}
	}
	
	public class PhotoTags
	{
		public String PhotoId {get; set;}
		public IList<String> Tags {get; set;}
	}
}
