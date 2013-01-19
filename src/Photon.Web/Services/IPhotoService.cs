using System;
using System.Collections.Generic;
using Photon.Web.Models;

namespace Photon.Web.Services
{
	public interface IPhotoService
	{
		Photo Load(string id);
		Photo Save(Photo photo);
		void Delete(String Id);

		IList<Photo> Recent(int limit);
		IList<Photo> FindByTags(IList<String> tags);
	}
}
