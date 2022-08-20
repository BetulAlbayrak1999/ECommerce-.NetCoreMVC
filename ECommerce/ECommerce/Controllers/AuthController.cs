using BussinessLogicLayer.Services.Abstract;
using BussinessLogicLayer.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        #region Register
        public IActionResult Register()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            try
            {
                if (model is null)
                    return View(model);

                var result = await _authService.RegisterAsync(model);
                if (result is not null && result.IsAuthenticated)
                    return RedirectToAction("Welcome", "Home");
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return View(model);
            }

        }
        #endregion
        #region

        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(TokenRequestVM model)
        {
            if (model is null)
                return RedirectToAction("Error", "Home");

            var result = await _authService.GetTokenAsync(model);
            if (result.IsAuthenticated == true)
                return RedirectToAction("Welcome", "Home");

            return RedirectToAction("Error", "Home");
        }

        #endregion

    }
}
