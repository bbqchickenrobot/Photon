using System;

namespace Photon.Web.Services.Exceptions
{
	public class DuplicateEntityException:ApplicationException
	{
		public DuplicateEntityException(String message):base(message)
		{
		}
	}
}
