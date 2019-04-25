using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppBootStrapMin.ViewModels;

namespace AppBootStrapMin.Controllers
{
    public class TB_MODELO_DOCController : Controller
    {
        private Model1 db = new Model1();

        // GET: TB_MODELO_DOC
        public async Task<ActionResult> Index()
        {
            return View(await db.TB_MODELO_DOC.ToListAsync());
        }

        // GET: TB_MODELO_DOC/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_MODELO_DOC tB_MODELO_DOC = await db.TB_MODELO_DOC.FindAsync(id);
            if (tB_MODELO_DOC == null)
            {
                return HttpNotFound();
            }
            return View(tB_MODELO_DOC);
        }

        // GET: TB_MODELO_DOC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TB_MODELO_DOC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_MODELO_DOC,ID_TP_ATO,ID_CTA_ACESSO_SIST,ID_USR_CAD,ID_USR_ALTER,DT_CAD,DT_ALTER,DESCRICAO,ARQUIVO,ARQ_BYTES,ATIVO")] TB_MODELO_DOC tB_MODELO_DOC)
        {
            if (ModelState.IsValid)
            {
                db.TB_MODELO_DOC.Add(tB_MODELO_DOC);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tB_MODELO_DOC);
        }

        // GET: TB_MODELO_DOC/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_MODELO_DOC tB_MODELO_DOC = await db.TB_MODELO_DOC.FindAsync(id);
            if (tB_MODELO_DOC == null)
            {
                return HttpNotFound();
            }
            return View(tB_MODELO_DOC);
        }

        // POST: TB_MODELO_DOC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_MODELO_DOC,ID_TP_ATO,ID_CTA_ACESSO_SIST,ID_USR_CAD,ID_USR_ALTER,DT_CAD,DT_ALTER,DESCRICAO,ARQUIVO,ARQ_BYTES,ATIVO")] TB_MODELO_DOC tB_MODELO_DOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_MODELO_DOC).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tB_MODELO_DOC);
        }

        // GET: TB_MODELO_DOC/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_MODELO_DOC tB_MODELO_DOC = await db.TB_MODELO_DOC.FindAsync(id);
            if (tB_MODELO_DOC == null)
            {
                return HttpNotFound();
            }
            return View(tB_MODELO_DOC);
        }

        // POST: TB_MODELO_DOC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            TB_MODELO_DOC tB_MODELO_DOC = await db.TB_MODELO_DOC.FindAsync(id);
            db.TB_MODELO_DOC.Remove(tB_MODELO_DOC);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
