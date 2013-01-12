
using System;

namespace Photon.Web.Models
{
	public class PhotonUser
	{
		public virtual String Id {get; set;}
		public virtual Guid ExternalId {get; set;}
		public virtual String Email{get; set;}
		public virtual String UserName {get; set;}
		public virtual String Password {get; set;}
		public virtual String Name {get; set;}
		public virtual UserStatus Status {get; set;}
		public virtual UserRole Role {get; set;}
	}
}
