﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataProtection;
using System.Web.Mvc;
using Infra.Cross.Identity.Configuration;
using Microsoft.AspNet.Identity.Owin;
using Infra.Cross.Identity.Models;

namespace Cartorio11RI
{
	public partial class Startup
	{
		private const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
		public static IDataProtectionProvider DataProtectionProvider { get; set; }

		// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		public void ConfigureAuth(IAppBuilder app)
		{

			//// Configure the db context, user manager and role manager to use a single instance per request
			app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());

			// Enable the application to use a cookie to store information for the signed in user
			// and to use a cookie to temporarily store information about a user logging in with a third party login provider
			// Configure the sign in cookie
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login"),
				Provider = new CookieAuthenticationProvider
				{
					// Enables the application to validate the security stamp when the user logs in.
					// This is a security feature which is used when you change a password or add an external login to your account.  
					OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
						validateInterval: TimeSpan.FromMinutes(30),
						regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
				},
				SlidingExpiration = true,
				ExpireTimeSpan = TimeSpan.FromMinutes(30)
			});

			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			// Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
			app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

			// Enables the application to remember the second login verification factor such as phone or email.
			// Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
			// This is similar to the RememberMe option when you log in.
			app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

			// Uncomment the following lines to enable logging in with third party login providers

			//microsoft
			//app.UseMicrosoftAccountAuthentication(
			//    clientId: "SEU ID",
			//    clientSecret: "SEU TOKEN");

			//google
			//app.UseGoogleAuthentication(
			//    clientId: "SEU ID",
			//    clientSecret: "SEU TOKEN");

			//twitter
			//app.UseTwitterAuthentication(
			//   consumerKey: "SEU ID",
			//   consumerSecret: "SEU TOKEN");

			//facebook
			//var facebookAutOptions = new FacebookAuthenticationOptions
			//{
			//    AppId = "SEU ID",
			//    AppSecret = "SEU TOKEN"
			//};

			//facebookAutOptions.Scope.Add("email");
			//facebookAutOptions.Scope.Add("publish_actions");
			//facebookAutOptions.Scope.Add("basic_info");

			//facebookAutOptions.Provider = new FacebookAuthenticationProvider()
			//{

			//    OnAuthenticated = (context) =>
			//    {
			//        context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
			//        foreach (var x in context.User)
			//        {
			//            var claimType = string.Format("urn:facebook:{0}", x.Key);
			//            string claimValue = x.Value.ToString();
			//            if (!context.Identity.HasClaim(claimType, claimValue))
			//                context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, XmlSchemaString, "Facebook"));

			//        }
			//        return Task.FromResult(0);
			//    }
			//};

			//facebookAutOptions.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
			//app.UseFacebookAuthentication(facebookAutOptions);

		}

	}
	
}