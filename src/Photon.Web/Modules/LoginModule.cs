
using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Authentication.Forms;
using Photon.Web.ViewModels;
using Photon.Web.Services;

namespace Photon.Web.Modules
{
	public class LoginModule:NancyModule
	{
		protected IUserAuthenticationService UserAuthenticationService { get; set; }
		protected IPhotonUserService PhotonUserService { get; set; }
		public LoginModule(IUserAuthenticationService userAuthenticationService, IPhotonUserService photonUserService)
			: base("login")
		{
			this.UserAuthenticationService = userAuthenticationService;
			this.PhotonUserService = photonUserService;

			Get["/"] = parameters => {
					return View["login", new LoginViewModel()];
			};
			Post["/"] = parameters =>
			{
				var loginModel = this.Bind<LoginViewModel>();
				var valid = UserAuthenticationService.ValidateUser(loginModel.UserName, loginModel.Password);
				if (valid)
				{
					DateTime? expiry = null;
					if (loginModel.RememberMe)
					{
						expiry = DateTime.Now.AddDays(7);
					}
					var userGuid = Guid.NewGuid();
					var user = this.PhotonUserService.GetUserByUserName(loginModel.UserName);
					user.ExternalId = userGuid;
					this.PhotonUserService.Save(user);
					return this.LoginAndRedirect(userGuid, expiry);
				}
				else
				{
					loginModel.ErrorMessage = "Invalid Username/Password.";
					return View["login", loginModel];
				}
				
			};
		}
	}
}
