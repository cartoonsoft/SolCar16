using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AdmCartorio.App_Start.Identity;
using AdmCartorio.Controllers.Base;
using AdmCartorio.Models.Identity;
using AdmCartorio.Models.Identity.Context;
using AdmCartorio.Models.Identity.Entities;
using AdmCartorio.ViewModels.Identity;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace AdmCartorio.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public UsuariosController()
        {

        }

        public UsuariosController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // Definindo a instancia UserManager presente no request.
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // Definindo a instancia SignInManager presente no request.
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //
            }
            base.Dispose(disposing);
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(UserManager.Users.ToList());
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            AccountRegisterViewModel accountRegisterViewModel = new AccountRegisterViewModel();
            return View(accountRegisterViewModel);
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Admin,Ativo,Nome,Username,Email,EmailConfirm,Password,PasswordConfirm,PhoneNumber")] AccountRegisterViewModel accountRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                string[] arrayAdminEmails = { "cris@cartoonsoft.com.br", "pedro.pires@cartoonsoft.com.br", "ronaldo.moreira@cartoonsoft.com.br" };
                var usuario = new ApplicationUser
                {
                    Nome = accountRegisterViewModel.Nome,
                    Ativo = true,
                    EmailConfirmed = true,
                    CreateDate = DateTime.Today,
                    UserName = accountRegisterViewModel.Email,
                    Email = accountRegisterViewModel.Email
                };

                if (arrayAdminEmails.Contains(usuario.Email) || (accountRegisterViewModel.Admin))
                {
                    usuario.EmailConfirmed = true;
                    usuario.CreateDate = DateTime.Now;

                    usuario.Claims.Add(new IdentityUserClaim
                    {
                        UserId = usuario.Id,
                        ClaimType = "AdminUsers",
                        ClaimValue = "true"
                    });
                }

                var result = await UserManager.CreateAsync(usuario, accountRegisterViewModel.Password);

                if (result.Succeeded)
                {
                    if (!usuario.EmailConfirmed)
                    {
                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(usuario.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = usuario.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(usuario.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'>Confirme sua Conta</a>");
                        ViewBag.Link = callbackUrl;
                    }
                    //return View("DisplayEmail");
                }
                else
                {
                    AddErrors(result);
                }

                return RedirectToAction("Index", "Usuarios");
            }

            return View(accountRegisterViewModel);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = UserManager.FindById(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(c => c.Type == "AdminUsers");

            AccountRegisterViewModel accountRegisterViewModel = new AccountRegisterViewModel
            {
                Id = Guid.Parse(applicationUser.Id),
                Nome = applicationUser.Nome,
                Admin = (claim != null),
                Ativo = applicationUser.Ativo,
                Username = applicationUser.UserName,
                Email = applicationUser.Email,
                PhoneNumber = applicationUser.PhoneNumber
            };

            return View(accountRegisterViewModel);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Admin,Ativo,Nome,Username,Email,EmailConfirm,Password,PasswordConfirm,PhoneNumber")] AccountRegisterViewModel accountRegisterViewModel)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser applicationUser = UserManager.FindById(accountRegisterViewModel.Id.ToString());

                if (applicationUser != null)
                {
                    if (!accountRegisterViewModel.Admin)
                    {
                        IdentityUserClaim claimAdmin = applicationUser.Claims.Where(c => c.ClaimType == "AdminUsers").FirstOrDefault();
                        applicationUser.Claims.Remove(claimAdmin);
                    }

                    applicationUser.Nome = accountRegisterViewModel.Nome;
                    applicationUser.AccessFailedCount = 0;
                    applicationUser.Ativo = accountRegisterViewModel.Ativo;
                    applicationUser.UserName = accountRegisterViewModel.Email;
                    applicationUser.Email = accountRegisterViewModel.Email;
                    applicationUser.LastPwdChangedDate = DateTime.Now;
                    applicationUser.PhoneNumber = accountRegisterViewModel.PhoneNumber;
                    applicationUser.PhoneNumberConfirmed = false;

                    await UserManager.UpdateAsync(applicationUser);
                    await UserManager.UpdateSecurityStampAsync(applicationUser.Id);
                    string resetToken = await UserManager.GeneratePasswordResetTokenAsync(applicationUser.Id);    
                    IdentityResult passwordChangeResult = await UserManager.ResetPasswordAsync(applicationUser.Id, resetToken, accountRegisterViewModel.Password);

                }
                return RedirectToAction("Index", "Usuarios");
            }

            return View(accountRegisterViewModel);
        }

        [HttpPost]
        public ActionResult AtivaDesativaUsuario(string id)
        {
            bool requestOk  = false; //operacao realizada com sucesso
            string msg = "";

            try
            {
                ApplicationUser user = UserManager.FindById(id);
                if (user == null)
                {
                    msg = "Usuário não cadastrado";
                }
                else
                {
                    user.Ativo = !user.Ativo;
                    UserManager.Update(user);
                    requestOk = true;
                    msg = "Usuário alaterado com sucesso!";
                }
            }
            catch (Exception ex)
            {
                requestOk = false;
                msg = "Falha na requisição! [" + ex.Message + "]";
            }

            var resultado = new
            {
                success = requestOk,
                mensagem = msg
            };

            return Json(resultado);
        }

    }
}
