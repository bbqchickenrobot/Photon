
using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Web.Extensions;
using Photon.Web.Models;
using Photon.Web.Services.Exceptions;
using Raven.Client;
using Raven.Client.Linq;

namespace Photon.Web.Services
{
	public class PhotoService : PhotonServiceBase<Photo>, IPhotoService
	{
		public PhotoService(IDocumentSession session)
			: base(session)
		{
		}

		public virtual IList<Photo> Recent(int limit)
		{
			return
				this.All()
				.OrderBy(a => a.ModifiedDate)
				.Take(limit)
				.ToList();
		}

		public virtual IList<Photo> FindByTags(IList<String> tags)
		{
			return this.All()
				.Where(a => a.Tags.Any(b => b.In(tags)))
				.ToList();
		}

		public virtual bool IsDuplicatePhoto(Photo photo)
		{
			return this.All()
				.Any(a => (a.Name == photo.Name || a.Path == photo.Path) && a.Id != photo.Id);
		}

		public override Photo Save(Photo photo)
		{
			var duplicatePhotoExists = this.IsDuplicatePhoto(photo);
			if (duplicatePhotoExists)
			{
				throw new DuplicateEntityException("Photo with name {0} already exists".FormatWith(photo.Name));
			}
			return base.Save(photo);
		}
	}
}
