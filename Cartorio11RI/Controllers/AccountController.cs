using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Infra.Cross.Identity.Configuration;
using Infra.Cross.Identity.Models;
using Infra.Cross.Identity.ViewModels;
using AppServCart11RI.AppServices;
using Domain.CartNew.Entities;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Domain.CartNew.Interfaces.UnitOfWork;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using Domain.CartNew.Enumerations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;

namespace Cartorio11RI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly long _idCtaAcessoSist;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUnitOfWorkDataBaseCartNew _ufwCartNew;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IUnitOfWorkDataBaseCartNew UfwCartNew = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ufwCartNew = UfwCartNew;
            this._idCtaAcessoSist = MvcApplication.IdCtaAcessoSist;

            //var a = _userManager.Users.ToList();
        }

        #region IDisposable Support
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            //MvcApplication.IdCtaAcessoSist

            base.Dispose(disposing);
        }
        #endregion

        #region privates methods
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        private void SetClaimsUsuario(string IdUsuario, GrupoUsuarioEnum grupo)
        {
            //remover all claims
            var claims = _userManager.GetClaims(IdUsuario);
            foreach (var claim in claims.Where(c => c.Type == ClaimTypes.Role))
            {
                _userManager.RemoveClaim(IdUsuario, claim);
            }

            // add claim
            switch (grupo)
            {
                case GrupoUsuarioEnum.Admin:
                    _userManager.AddClaim(IdUsuario, new Claim(ClaimTypes.Role, "Admin"));
                    break;
                case GrupoUsuarioEnum.GerenteRI:
                    _userManager.AddClaim(IdUsuario, new Claim(ClaimTypes.Role, "GerenteRI"));
                    break;
                case GrupoUsuarioEnum.UsuarioRI:
                    _userManager.AddClaim(IdUsuario, new Claim(ClaimTypes.Role, "UsuarioRI"));
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Async Methods
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Login ou Senha incorretos.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await _signInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(await _signInManager.GetVerifiedUserIdAsync());

            if (user != null)
            {
                //ViewBag.Status = "DEMO: Caso o código não chegue via " + provider + " o código é: ";
                //ViewBag.CodigoAcesso = await _userManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Código Inválido.");
                    return View(model);
            }
        }

        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "Id,Nome,UserName,Email,EmailConfirm,Password,ConfirmPassword,PhoneNumber,Ativo,GrupoUsuario")]RegisterViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioViewModel);
            }

            var usr = _userManager.FindByName(usuarioViewModel.UserName);

            if (usr != null)
            {
                ModelState.AddModelError(usuarioViewModel.UserName, string.Format("Usuário {0} já existe na base!", usuarioViewModel.UserName));
                return View(usuarioViewModel);
            }

            var usuario = new ApplicationUser
            {
                IdCtaAcessoSist = MvcApplication.IdCtaAcessoSist,
                Nome = usuarioViewModel.Nome,
                UserName = usuarioViewModel.UserName,
                CreateDate = DateTime.Now,
                Email = usuarioViewModel.Email,
                EmailConfirmed = true,
                PhoneNumber = usuarioViewModel.PhoneNumber,
                PhoneNumberConfirmed = true,
                LockoutEnabled = true, //If you set LockoutEnabled to true and add a LockoutEnd date, you'll prevent that user from logging in again until after the LockoutEnd date is reached.
                Ativo = true
            };

            var result = await _userManager.CreateAsync(usuario, usuarioViewModel.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(usuario, isPersistent: false, rememberBrowser: false);
                //var claimsIdentity = await _userManager.CreateIdentityAsync(usuario, DefaultAuthenticationTypes.ApplicationCookie);
                SetClaimsUsuario(usuario.Id, usuarioViewModel.GrupoUsuario);

                return RedirectToAction("Index", "Account");

                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(usuario.Id);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = usuario.Id, code = code }, protocol: Request.Url.Scheme);
                //await _userManager.SendEmailAsync(usuario.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'></a>");
                //ViewBag.Link = callbackUrl;
                //return View("DisplayEmail");
            } else
            {
                AddErrors(result);
                return View(usuarioViewModel);
            }
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,UserName,Email,EmailConfirm,Password,ConfirmPassword,PhoneNumber,Ativo,GrupoUsuario")]RegisterViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = _userManager.FindById(usuarioViewModel.Id.ToString());

                if (applicationUser != null)
                {
                    applicationUser.Nome = usuarioViewModel.Nome;
                    applicationUser.UserName = usuarioViewModel.UserName;
                    applicationUser.Email = usuarioViewModel.Email;
                    applicationUser.AccessFailedCount = 0;
                    applicationUser.Ativo = usuarioViewModel.Ativo;
                    applicationUser.LastPwdChangedDate = DateTime.Now;
                    applicationUser.PhoneNumber = usuarioViewModel.PhoneNumber;
                    applicationUser.PhoneNumberConfirmed = false;

                    await _userManager.UpdateAsync(applicationUser);
                    await _userManager.UpdateSecurityStampAsync(applicationUser.Id);
                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(applicationUser.Id);
                    IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(applicationUser.Id, resetToken, usuarioViewModel.Password);

                    SetClaimsUsuario(applicationUser.Id, usuarioViewModel.GrupoUsuario);
                    return RedirectToAction("Index", "Account");
                }
            }

            return View(usuarioViewModel);
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Não revelar se o usuario nao existe ou nao esta confirmado
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Esqueci minha senha", "Por favor altere sua senha clicando aqui: <a href='" + callbackUrl + "'></a>");
                ViewBag.Link = callbackUrl;
                ViewBag.Status = "DEMO: Caso o link não chegue: ";
                ViewBag.LinkAcesso = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }

            // No caso de falha, reexibir a view. 
            return View(model);
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Não revelar se o usuario nao existe ou nao esta confirmado
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await _signInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await _signInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // Se ele nao tem uma conta solicite que crie uma
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Pegar a informação do login externo.
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        #endregion

        public long IdCtaAcessoSist
        {
            get { return this._idCtaAcessoSist; }
        }

        // GET: /Account/Index
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: /Account/Register
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            RegisterViewModel usuarioViewModel = new RegisterViewModel();

            return View(usuarioViewModel);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "IdUsuário é null ou inválido");
            }

            ApplicationUser applicationUser = _userManager.FindById(id);

            if (applicationUser == null)
            {
                return HttpNotFound("Usuário não foi encontrado na base de dados!");
            }

            RegisterViewModel usuarioViewModel = new RegisterViewModel
            {
                Id = Guid.Parse(applicationUser.Id),
                Nome = applicationUser.Nome,
                Ativo = applicationUser.Ativo,
                UserName = applicationUser.UserName,
                Email = applicationUser.Email,
                PhoneNumber = applicationUser.PhoneNumber,
            };

            var claims = _userManager.GetClaims(id).ToList();

            if (claims.Find(c =>  (c.Type == ClaimTypes.Role) && (c.Value == "Admin")) != null)
            {
                usuarioViewModel.GrupoUsuario = GrupoUsuarioEnum.Admin;
            } else if (claims.Find(c => (c.Type == ClaimTypes.Role) && (c.Value == "GerenteRI")) != null)
            {
                usuarioViewModel.GrupoUsuario = GrupoUsuarioEnum.GerenteRI;
            } else if (claims.Find(c => (c.Type == ClaimTypes.Role) && (c.Value == "UsuarioRI")) != null)
            {
                usuarioViewModel.GrupoUsuario = GrupoUsuarioEnum.UsuarioRI;
            }

            return View(usuarioViewModel);
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }


        // POST: /Account/LogOff
        //[HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            if (AuthenticationManager != null)
            {
                AuthenticationManager.SignOut();
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult MontarMenuUsuario(string IdUsuario)
        {
            ViewBag.CurrentControler = ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString().ToLower();
            ViewBag.CurrentAction    = ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString().ToLower();

            IEnumerable<DtoMenuAcaoList> Menu = new List<DtoMenuAcaoList>();
            ApplicationUser usrApp = _userManager.FindById(IdUsuario);
            var claims = _userManager.GetClaims(IdUsuario).ToList();

            if (usrApp != null)
            {
                UsuarioIdentity usr = new UsuarioIdentity()
                {
                    Id = usrApp.Id,
                    IdCtaAcessoSist = usrApp.IdCtaAcessoSist,
                    AccessFailedCount = usrApp.AccessFailedCount,
                    UserName = usrApp.UserName,
                    Email = usrApp.Email,
                    Nome = usrApp.Nome,
                    Ativo = usrApp.Ativo,
                    CreateDate = usrApp.CreateDate,
                    EmailConfirmed = usrApp.EmailConfirmed,
                    LastLockoutDate = usrApp.LastLockoutDate,
                    LastLoginDate = usrApp.LastLoginDate,
                    LastPwdChangedDate = usrApp.LastPwdChangedDate,
                    LockoutEnabled = usrApp.LockoutEnabled,
                    PhoneNumber = usrApp.PhoneNumber,
                    PhoneNumberConfirmed = usrApp.PhoneNumberConfirmed,
                    TwoFactorEnabled = usrApp.TwoFactorEnabled
                };

                using (AppServiceAcoesUsuarios appService = new AppServiceAcoesUsuarios(this._ufwCartNew, this.IdCtaAcessoSist))
                {
                    Menu = appService.GetListMenuUsuario(usr, this.IdCtaAcessoSist);
                }
            }

            return PartialView("_MenuUsuario", Menu);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AtivaDesativaUsuario(string id, string cmd)
        {
            bool requestOk = false; //operacao realizada com sucesso
            string msg = "";

            try
            {
                ApplicationUser user = _userManager.FindById(id);
                if (user == null)
                {
                    msg = "Usuário não cadastrado";
                }
                else
                {
                    user.Ativo = !user.Ativo;
                    _userManager.Update(user);
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

        [HttpPost]
        public JsonResult ConfirmarUserLoginSenha(string usuario, string pass)
        {
            bool resp = false;
            string message = string.Empty;
            string idUserTmp = string.Empty;

            try
            {
                var user = _userManager.Find(usuario, pass);

                if (user != null)
                {
                    resp = true;
                    idUserTmp = user.Id;
                    message = "Usuário Confirmado com sucesso!";
                } else {
                    message = "Usuário não confirmado!";
                }
            }
            catch (Exception ex)
            {
                message = string.Format("Falha confirmação de usuário, erro: {0}", ex.Message);

            }

            var resultado = new
            {
                resposta = resp,
                msg = message,
                idUsuario = idUserTmp
            };

            return Json(resultado);
        }

    }
}