using AdmCartorio.App_Start.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;

namespace AdmCartorio.Models.Identity.Context
{
    public class DataBaseIInitializerCartorio: DropCreateDatabaseAlways<ApplicationDbContextIdentity>
    {

        protected override void Seed(ApplicationDbContextIdentity context)
        {

            using (IdentityInit initDb = new IdentityInit())
            {
                //initDb.InsereUsuariosAdmin(context);
            }

            base.Seed(context);

        }
    }
}