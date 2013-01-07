
using System;
using System.Collections.Generic;

namespace Photon.Web.Models
{
	public class Photo
	{
		public String Id {get; set;}
		public String Name {get; set;}
		public String Path {get; set;}
		public IList<String> Tags {get; set;}
		public Album Album {get; set;}
		public DateTime ModifiedDate{get; set;}
		public bool IsPrivate{get; set;}
	}
}
