﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppServices.Car16.Interfaces;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Interfaces.UnitOfWork;

namespace AppBootStrapMin.Controllers
{
    public class MatriculasController : Controller
    {
        private readonly IUnitOfWorkCar16 _unitOfWorkCar16;
        private readonly IAppServicePais _appServicePais;

        public MatriculasController(IUnitOfWorkCar16 unitOfWorkCar16, IAppServicePais appServicePais)
        {
            this._unitOfWorkCar16 = unitOfWorkCar16;
            this._appServicePais = appServicePais;
        }

        // GET: Matriculas
        public ActionResult Index()
        {
            List<Matricula> Matriculas = this._unitOfWorkCar16.Repositories.GenericRepository<Matricula>().GetAll().Take(100).ToList();
            //var Matriculas = _appServicePais.GetAll().ToList();

            return View(Matriculas);
        }

        // GET: Matriculas/Details/5
        public ActionResult Details(long? NUMERO, long? SEQINC)
        {
            if ((NUMERO == null) || (SEQINC == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matricula matricula = _unitOfWorkCar16.Repositories.GenericRepository<Matricula>().GetById(new Object[] { NUMERO, SEQINC });

            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // GET: Matriculas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Matriculas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NUMERO,SEQINC,ENTRADA,SAIDA,MOTIVO,RESPONSAVEL,HORAENTRADA,HORASAIDA")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                _unitOfWorkCar16.Repositories.GenericRepository<Matricula>().Add(matricula);
                _unitOfWorkCar16.Commit();
                return RedirectToAction("Index");
            }

            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public ActionResult Edit(long? NUMERO, long? SEQINC)
        {
            if ((NUMERO == null) || (SEQINC == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Matricula matricula = _unitOfWorkCar16.Repositories.GenericRepository<Matricula>().GetById(new Object[] { NUMERO, SEQINC } );
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // POST: Matriculas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NUMERO,SEQINC,ENTRADA,SAIDA,MOTIVO,RESPONSAVEL,HORAENTRADA,HORASAIDA")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                _unitOfWorkCar16.Repositories.GenericRepository<Matricula>().Update(matricula);
                _unitOfWorkCar16.Commit();
                return RedirectToAction("Index");
            }
            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public ActionResult Delete(long? NUMERO, long? SEQINC)
        {
            if ((NUMERO == null) || (SEQINC == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Matricula matricula = _unitOfWorkCar16.Repositories.GenericRepository<Matricula>().GetById(new Object[] { NUMERO, SEQINC });
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Matricula matricula = _unitOfWorkCar16.Repositories.GenericRepository<Matricula>().GetById(id);

            if (matricula != null)
            {
                _unitOfWorkCar16.Repositories.GenericRepository<Matricula>().Remove(matricula);
                _unitOfWorkCar16.Commit();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_unitOfWorkCar16 != null)
                {
                    _unitOfWorkCar16.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}