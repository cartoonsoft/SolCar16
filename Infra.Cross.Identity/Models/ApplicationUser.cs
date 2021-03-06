﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infra.Cross.Identity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    [Table("AspNetUsers", Schema = "DEZESSEIS_NEW")]
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Clients = new Collection<Client>();
        }
        public long IdCtaAcessoSist { get; set; }

        [NotMapped]
        public string IdUsuario
        {
            get { return this.Id; }
        }

        [NotMapped]
        public string  LoginName 
        {
            get { return this.UserName; }
        } 

        public virtual ICollection<Client> Clients { get; set; }

        [NotMapped]
        public string CurrentClientId { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastLoginDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastPwdChangedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastLockoutDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, ClaimsIdentity ext = null)
        {
            // Observe que o authenticationType precisa ser o mesmo que foi definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(CurrentClientId))
            {
                claims.Add(new Claim("AspNet.Identity.ClientId", CurrentClientId));
            }

            //  Adicione novos Claims aqui //

            // Adicionando Claims externos capturados no login
            if (ext != null)
            {
                await SetExternalProperties(userIdentity, ext);
            }

            // Gerenciamento de Claims para informaçoes do usuario
            //claims.Add(new Claim("AdmRoles", "True"));

            userIdentity.AddClaims(claims);

            return userIdentity;
        }

        private async Task SetExternalProperties(ClaimsIdentity identity, ClaimsIdentity ext)
        {
            if (ext != null)
            {
                var ignoreClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
                // Adicionando Claims Externos no Identity
                foreach (var c in ext.Claims)
                {
                    if (!c.Type.StartsWith(ignoreClaim))
                        if (!identity.HasClaim(c.Type, c.Value))
                            identity.AddClaim(c);
                }
            }
        }
    }
}