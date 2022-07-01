using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ristorante.ProvaMVC.Core.BusinessLayer;
using Ristorante.ProvaMVC.WebbAppMVC.Helper;
using Ristorante.ProvaMVC.Core.Entities;
using Ristorante.ProvaMVC.WebbAppMVC.Models;

namespace Ristorante.ProvaMVC.WebbAppMVC.Controllers
{

    [Authorize]
    public class MenuController : Controller
    {
        private readonly IBusinessLayer BL;

        public MenuController(IBusinessLayer bl)
        {
            BL = bl;
        }
        public IActionResult Index()
        {
            List<Menu> menu = BL.GetAllMenu();
            List<MenuViewModel> menuViewModel = new List<MenuViewModel>();
            foreach (var item in menu)
            {
                menuViewModel.Add(item.ToMenuViewModel());
            }
            return View(menuViewModel);
        }

        public IActionResult Details(int id)
        {
            var menu = BL.GetAllMenu().FirstOrDefault(x => x.Id == id);
            return View(menu.ToMenuViewModel());
        }
        [Authorize(Policy = "Ristoratore")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "Ristoratore")]
        [HttpPost]
        public IActionResult Create(MenuViewModel menuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu menu = menuViewModel.ToMenu();
                Esito esito = BL.AggiungiMenu(menu);
                if (esito.IsOk)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.MessaggioErrore = esito.Messaggio;
                    return View("ErroriDiBusiness");
                }
            }
            return View(menuViewModel);
        }
        /*
        [Authorize(Policy = "Ristoratore")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var menu = BL.GetAllMenu().FirstOrDefault(x => x.Id == id);
            var menuViewModel = menu.ToMenuViewModel();
            return View(menuViewModel);
        }
        [Authorize(Policy = "Ristoratore")]
        [HttpPost]
        public IActionResult Edit(MenuViewModel menuViewModel)
        {
            if (ModelState.IsValid)
            {
                var menu = menuViewModel.ToMenu();
                var esito = BL.ModificaMenu(menu);
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
            return View(menuViewModel);
        }
        [Authorize(Policy = "Ristoratore")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var menu = BL.GetAllMenu().FirstOrDefault(x => x.Id == id);
            var menuViewModel = menu.ToMenuViewModel();
            return View(menuViewModel);
        }
        [Authorize(Policy = "Ristoratore")]
        [HttpPost]
        public IActionResult Delete(MenuViewModel menuViewModel)
        {
            if (ModelState.IsValid)
            {
                var menu = menuViewModel.ToMenu();
                var esito = BL.RimuoviMenu(menu);
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
            return View(menuViewModel);
        }*/
        [Authorize(Policy = "Ristoratore")]
        [HttpGet]
        public IActionResult Decouple(int id)
        {
            var piatto = BL.GetAllPiatti().FirstOrDefault(x => x.Id == id);
            var menu = BL.GetAllMenu().FirstOrDefault(x => x.Id == piatto.IdMenu);
            var menuViewModel = menu.ToMenuViewModel();
            ViewBag.Menu = menuViewModel;
            return View(piatto.ToPiattoViewModel());
        }
        [Authorize(Policy = "Ristoratore")]
        [HttpPost]
        public IActionResult Decouple(PiattoViewModel piattoViewModel)
        {
            if (ModelState.IsValid)
            {
                var piatto = piattoViewModel.ToPiatto();
                var menu = BL.GetAllPiatti().FirstOrDefault(x => x.Id == piatto.Id).Menu;
                var esito = BL.RimuoviPiattoMenu(piatto, menu);
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
    }
}
