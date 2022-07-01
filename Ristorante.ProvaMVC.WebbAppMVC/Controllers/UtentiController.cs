using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ristorante.ProvaMVC.Core.BusinessLayer;
using Ristorante.ProvaMVC.WebbAppMVC.Models;

namespace Ristorante.ProvaMVC.WebbAppMVC.Controllers
{
    [Authorize]
    public class UtentiController : Controller
    {
        private readonly IBusinessLayer BL;

        public UtentiController(IBusinessLayer bl)
        {
            BL = bl;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new UtenteLoginViewModel { ReturnUrl = returnUrl });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UtenteLoginViewModel utenteLoginViewModel)
        {
            if (utenteLoginViewModel == null)
            {
                return View();
            }

            var utente = BL.GetAccount(utenteLoginViewModel.Username);
            if (utente != null && ModelState.IsValid)
            {
                if (utente.Password == utenteLoginViewModel.Password)
                {
                    //l'utente è autenticato"
                    var claim = new List<Claim>{

                        new Claim (ClaimTypes.Name, utente.Username),
                        new Claim (ClaimTypes.Role, utente.Role.ToString())
                    };

                    var properties = new AuthenticationProperties
                    {
                        RedirectUri = utenteLoginViewModel.ReturnUrl,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) //logout dopo un'ora di inattività
                    };

                    var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimIdentity),
                        properties
                        );
                    return Redirect("/Home/index");
                }
                else
                {
                    ModelState.AddModelError(nameof(utenteLoginViewModel.Password), "Password errata");
                    return View(utenteLoginViewModel);
                }
            }
            else
            {
                ModelState.AddModelError(nameof(utenteLoginViewModel.Username), "Username inesistente");
                return View(utenteLoginViewModel);
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Forbidden() => View();

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
