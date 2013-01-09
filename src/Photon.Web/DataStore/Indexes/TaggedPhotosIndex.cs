using System;
using System.Linq;
using System.Collections.Generic;
using Photon.Web.Models;
using Raven.Client.Indexes;

namespace Photon.Web.DataStore.Indexes
{
	public class TaggedPhotosIndex:AbstractIndexCreationTask<Album, TaggedPhotos>
	{
		public TaggedPhotosIndex()
		{
			Map = albums =>
				from a in albums
				from p in a.Photos
				from t in p.Tags
				select new TaggedPhotos
				{
					Tag = t,
					PhotoIds = new List<String> {p.Id}
				};
			Reduce = results => 
				from result in results
				group result by result.Tag into agg
				select new TaggedPhotos
				{
					Tag = agg.Key,
					PhotoIds = agg.SelectMany(a => a.PhotoIds).ToList()
				};
		}
	}
	
	public class TaggedPhotos
	{
		public String Tag {get; set;}
		public IList<String> PhotoIds {get; set;}
	}
}
