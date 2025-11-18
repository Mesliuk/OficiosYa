using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OficiosYa.Api.Controllers
{
    public class OficioController : Controller
    {
        // GET: OficioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OficioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OficioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OficioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OficioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OficioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OficioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OficioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
