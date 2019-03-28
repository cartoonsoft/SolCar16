using AdmCartorio.Models.Identity;
using AdmCartorio.Models.Identity.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AdmCartorio.App_Start.Identity
{
    public class IdentityInit : IDisposable
    {

        private readonly string users = "{\"usuarios\":[" +
            "{\"Nome\":\"Ana Cristina Aoki\",\"Email\":\"cris@cartoonsoft.com.br\"}," +
            "{\"Nome\":\"Pedro Pires\",\"Email\":\"pedro.pires@cartoonsoft.com.br\"}," +
            "{\"Nome\":\"Ronaldo Moreira\",\"Email\":\"ronaldo.moreira@cartoonsoft.com.br\"}," +
        "]}";

        private string pass = "";


        public IdentityInit()
        {
            //
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~IdentityInit() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public async Task InsereUsuariosAdminAsync(ApplicationUserManager userManager)
        {
            JObject usersData = JObject.Parse(users);
            foreach (var userData in usersData["usuarios"])
            {
                ApplicationUser usuario = new ApplicationUser
                {
                    UserName = (string)userData["Nome"],
                    Email = (string)userData["Email"],
                    EmailConfirmed = true
                    //SecurityStamp = Guid.NewGuid().ToString()
                };

                usuario.Claims.Add(new IdentityUserClaim
                {
                    UserId = usuario.Id,
                    ClaimType = "AdminUsers",
                    ClaimValue = "true"
                });

                var result = await userManager.CreateAsync(usuario, pass);
            }

        }


    }
}