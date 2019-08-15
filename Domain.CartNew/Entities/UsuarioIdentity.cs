using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities
{
    public class UsuarioIdentity
    {
        public UsuarioIdentity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public  string Id                          { get; set; }
        public virtual long IdCtaAcessoSist        { get; set; }
        public virtual bool Ativo                  { get; set; }
        public virtual string UserName             { get; set; }
        public virtual string Nome                 { get; set; }
        public virtual DateTime CreateDate         { get; set; }
        public virtual DateTime LastLoginDate      { get; set; }
        public virtual DateTime LastPwdChangedDate { get; set; }
        public virtual DateTime LastLockoutDate    { get; set; }
        public virtual string Email                { get; set; }
        public virtual bool EmailConfirmed         { get; set; }
        public virtual string PasswordHash         { get; set; }
        public virtual string SecurityStamp        { get; set; }
        public virtual string PhoneNumber          { get; set; }
        public virtual bool PhoneNumberConfirmed   { get; set; }
        public virtual bool TwoFactorEnabled       { get; set; }
        public virtual DateTime LockoutEndDateUtc  { get; set; }
        public virtual bool LockoutEnabled         { get; set; }
        public virtual int AccessFailedCount       { get; set; }
    }
}
