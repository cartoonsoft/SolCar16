using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdmCartorio.Controllers.Base;
using AdmCartorio.Models.Identity;
using AdmCartorio.Models.Identity.Context;
using AdmCartorio.Models.Identity.Entities;
using Domain.Car16.Interfaces.UnitOfWork;

namespace AdmCartorio.Controllers
{
    public class UsuariosController : AdmCartorioBaseController
    {
        private ApplicationDbContextIdentity dbContext = new ApplicationDbContextIdentity();

        //colocar aqui Appp services 

        public UsuariosController(IUnitOfWorkDataBseCar16 unitOfWorkDataBseCar16, IUnitOfWorkDataBaseCar16New unitOfWorkDataBseCar16New): base (unitOfWorkDataBseCar16, unitOfWorkDataBseCar16New)
        {
            
            //this.UnitOfWorkDataBseCar16
            
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(dbContext.Users.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = dbContext.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Nome,CreateDate,LastLoginDate,LastPwdChangedDate,LastLockoutDate")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                dbContext.Users.Add(applicationUser);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = dbContext.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(applicationUser).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = dbContext.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = dbContext.Users.Find(id);
            dbContext.Users.Remove(applicationUser);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
