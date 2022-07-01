using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ristorante.ProvaMVC.Core.BusinessLayer;
using Ristorante.ProvaMVC.WebbAppMVC.Helper;
using Ristorante.ProvaMVC.WebbAppMVC.Models;

namespace Ristorante.ProvaMVC.WebbAppMVC.Controllers
{
    [Authorize]
    public class PiattiController : Controller
    {
        private readonly IBusinessLayer BL;

        public PiattiController(IBusinessLayer bl)
        {
            BL = bl;
        }
        public IActionResult Index()
        {
            var piatti = BL.GetAllPiatti();
            List<PiattoViewModel> piattoViewModel = new List<PiattoViewModel>();
            foreach (var item in piatti)
            {
                piattoViewModel.Add(item.ToPiattoViewModel());
            }
            return View(piattoViewModel);
        }

        [Authorize(Policy = ("Ristoratore"))]
        [HttpGet]
        public IActionResult Assign(int id)
        {
            var piatto = BL.GetAllPiatti().FirstOrDefault(p => p.Id == id);
            var piattoViewModel = piatto.ToPiattoViewModel();
            var menuList = BL.GetAllMenu();
            ViewBag.Menu = new SelectList(from m in menuList
                                               select new { Value = m.Id, Text = m.Name }
            , "Value", "Text");
            return View(piattoViewModel);
        }
        [Authorize(Policy = ("Ristoratore"))]
        [HttpPost]
        public IActionResult Assign(PiattoViewModel piattoVm)
        {
            if (ModelState.IsValid)
            {
                var piatto = piattoVm.ToPiatto();
                var menu = BL.GetAllMenu().FirstOrDefault(x => x.Id == piatto.IdMenu);
                var esito = BL.AggiungiPiattoMenu(piatto, menu);
                if (esito.IsOk == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.MessaggioErrore = esito.Messaggio;
                    return View("ErroriDiBusiness");
                }
            }
            return View(piattoVm);
        }
        [Authorize(Policy = ("Ristoratore"))]
        [HttpGet]
        public IActionResult Create()
        {
            LoadViewBag();
            return View();
        }

        [Authorize(Policy = ("Ristoratore"))]
        [HttpPost]
        public IActionResult Create(PiattoViewModel piattoViewModel)
        {
            if (ModelState.IsValid)
            {
                var piatto = piattoViewModel.ToPiatto();
                var esito = BL.AggiungiPiatto(piatto);
                if (esito.IsOk == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.MessaggioErrore = esito.Messaggio;
                    return View("ErroriDiBusiness");
                }
            }
            else
            {
                LoadViewBag();
                return View(piattoViewModel);
            }
        }
        [Authorize(Policy = ("Ristoratore"))]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var piatto = BL.GetAllPiatti().FirstOrDefault(p => p.Id == id);
            var piattoViewModel = piatto.ToPiattoViewModel();
            LoadViewBag();
            return View(piattoViewModel);
        }
        [Authorize(Policy = ("Ristoratore"))]
        [HttpPost]
        public IActionResult Edit(PiattoViewModel piattoViewModel)
        {
            if (ModelState.IsValid)
            {
                var piatto = piattoViewModel.ToPiatto();
                var esito = BL.ModificaPiatto(piatto);
                if (esito.IsOk == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.MessaggioErrore = esito.Messaggio;
                    return View("ErroriDiBusiness");
                }
            }
            return View(piattoViewModel);
        }
        [Authorize(Policy = ("Ristoratore"))]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var piatto = BL.GetAllPiatti().FirstOrDefault(p => p.Id == id);
            var piattoViewModel = piatto.ToPiattoViewModel();

            return View(piattoViewModel);
        }
        [Authorize(Policy = ("Ristoratore"))]
        [HttpPost]
        public IActionResult Delete(PiattoViewModel piattoViewModel)
        {
            var piatto = piattoViewModel.ToPiatto();
            var esito = BL.RimuoviPiatto(piatto);
            if (esito.IsOk == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.MessaggioErrore = esito.Messaggio;
                return View("ErroriDiBusiness");
            }
        }
        private void LoadViewBag()
        {
            ViewBag.Tipologia = new SelectList(new[]
            {
                new{Value="Primo", Text="Primo"},
                new{Value="Secondo", Text="Secondo"},
                new{Value="Contorno", Text="Contorno"},
                new{Value="Dolce", Text="Dolce"}
            }, "Value", "Text");
        }
    }
}
