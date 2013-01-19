
using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Imports.Newtonsoft.Json;

namespace Photon.Web.Models
{
	[JsonObject(IsReference = true)]
	public class Album
	{
		public Album()
		{
			this.Photos = new List<String>();
			this.Tags = new List<String>();
		}

		public String Id { get; set; }
		public String Name { get; set; }
		public DateTime ModifiedDate { get; set; }
		public IList<String> Tags { get; set; }
		public IList<String> Photos { get; set; }
		public String AlbumCover { get; set; }

		public VisibilityType VisibilityType { get; set; }
	}
}
