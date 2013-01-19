using AutoMapper;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photon.Web.Extensions;
using Photon.Web.Models;
using Photon.Web.Services;
using Raven.Client;

namespace Photon.Web.Modules
{
	public class DevUtils : NancyModule
	{
		protected virtual IDocumentSession RavenSession { get; set; }
		public DevUtils(IDocumentSession session)
			: base("_devutils")
		{
			this.RavenSession = session;
			Get["/"] = parameters =>
			{
				return View["DevUtils"];
			};

			Post["/load-sample-data"] = parameters =>
			{
				if (!HttpContext.Current.Request.IsLocal)
				{
					return HttpStatusCode.NotFound;
				}
				String passcode = Request.Form["passcode"];
				var model = new DevUtilsResponseModel();
				if (String.IsNullOrEmpty(passcode))
				{
					model.ResultCode = -99;
					model.Message = "Missing passcode";
				}
				else if (passcode.ToMD5Hash() != "5f4dcc3b5aa765d61d8327deb882cf99")
				{
					model.ResultCode = -100;
					model.Message = "Invalid passcode";
				}
				else if (RavenSession.Query<PhotonUser>().Any(a => a.UserName == "admin"))
				{
					model.ResultCode = -101;
					model.Message = "Data already exists!!!";
				}
				else
				{
					model.ResultCode = 0;
					model.Message = "Sample data loaded successfully!!!";
					var photonUser = new PhotonUser
					{
						UserName = "admin",
						Email = "admin@admin.com",
						Password = "admin".ToMD5Hash(),
						Name = "Admin",
						Role = UserRole.Admin,
						Status = UserStatus.Active
					};
					for (var i = 0; i < 10; i++)
					{
						var album = new Album
						{
							AlbumCover = "Some Path",
							Name = i.ToString() + "Album",
							VisibilityType = VisibilityType.Public,
							Tags = new[] { "test-album" }
						};
						RavenSession.Store(album);
					}
					RavenSession.Store(photonUser);
					RavenSession.SaveChanges();
				}
				return Response.AsJson(model);
			};
		}
	}

	public class DevUtilsResponseModel
	{
		public String Message { get; set; }
		public int ResultCode { get; set; }
	}
}