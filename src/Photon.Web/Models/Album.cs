
using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Imports.Newtonsoft.Json;

namespace Photon.Web.Models
{
	[JsonObject(IsReference=true)]
	public class Album
	{
		public Album()
		{
			this.Photos = new List<Photo>();
			this.Tags = new List<String>();
		}
		
		public String Id {get; set;}
		public String Name {get; set;}
		public String Path {get; set;}
		public DateTime ModifiedDate{get; set;}
		public IList<String> Tags {get; set;}
		public IList<Photo> Photos {get; set;}
		
		public VisibilityType VisibilityType
		{
			get
			{
				return 
					(this.Photos.Any() && !this.Photos.Any(a => !a.IsPrivate))?VisibilityType.Private:VisibilityType.Public;
			}
		}
	}
}
