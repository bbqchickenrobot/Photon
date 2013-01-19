using Nancy;
using Photon.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;
using Photon.Web.Models;
using Photon.Web.Core;

namespace Photon.Web.Modules
{
	public class AlbumsModule : NancyModule
	{
		protected IAlbumService AlbumService { get; set; }
		public AlbumsModule(IAlbumService albumService)
			: base("albums")
		{
			this.AlbumService = albumService;


			Get["/"] = parameters =>
			{
				var albums = this.AlbumService.All();
				return Response.AsJson(albums);
			};

			Get["/{id}"] = parameters =>
			{
				String albumId = parameters.id;
				var album = this.AlbumService.Load(albumId);
				return Response.AsJson(album);
			};

			Post["/"] = parameters =>
			{

				try
				{
					var album = this.Bind<Album>();
					this.AlbumService.Save(album);

					return Response.AsJson(new
					{
						ResultCode = 0,
						Message = "Album saved successfully"
					});
				}
				catch (Exception e)
				{
					return Response.AsJson(new
					{
						ResultCode = -99,
						Message = e.Message
					});
				}
			};

			Delete["/{id}"] = parameters =>
			{
				try
				{
					String albumId = parameters.id;
					this.AlbumService.Delete(albumId);

					return Response.AsJson(new
					{
						ResultCode = 0,
						Message = "Album deleted successfully"
					});
				}
				catch (Exception e)
				{
					return Response.AsJson(new
					{
						ResultCode = -99,
						Message = e.Message
					});
				}
			};
		}

	}
}