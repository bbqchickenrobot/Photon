
using System;

namespace Photon.Web.Models
{
	[Flags]
	public enum VisibilityType : int
	{
		Public,
		Private
	}

	[Flags]
	public enum UserStatus : int
	{
		InActive,
		Active
	}

	[Flags]
	public enum UserRole : int
	{
		Guest,
		SiteUser,
		Admin
	}
}
