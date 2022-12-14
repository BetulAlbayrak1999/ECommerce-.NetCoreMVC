using AutoMapper;
using BussinessLogicLayer.Dtos.OrderDtos;
using BussinessLogicLayer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _autoMapper;
        public OrderController(IOrderService OrderService, IMapper autoMapper)
        {
            _orderService = OrderService;
            _autoMapper = autoMapper;
        }

        #region Index
        public IActionResult Index()
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
        #endregion


        #region GetAllActivated
        public async Task<IActionResult> GetAllActivated()
        {
            try
            {
                var result = await _orderService.GetAllActivatedAsync();
                return View(result);

            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region GetAll
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _orderService.GetAllAsync();
                if (result is not null)
                    return View(result);
                return RedirectToAction("Error", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region GetAllUnActivated
        public async Task<IActionResult> GetAllUnActivated()
        {
            try
            {
                var result = await _orderService.GetAllUnActivatedAsync();
                return View(result);

            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var result = await _orderService.DeleteAsync(Id);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        #endregion


        #region Create
        public async Task<ActionResult> Create()
        {
            try
            {
                var productDB = await _orderService.GetAllActivatedProduct();
                if (productDB == null)
                    return View("Index");
                List<SelectListItem> products = (from x in productDB
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.Id.ToString()
                                                 }).ToList();
                ViewBag.products = products;

                var applicationUserDB = await _orderService.GetAllApplicationUser();
                if (applicationUserDB == null)
                    return View("Index");
                List<SelectListItem> applicationUsers = (from x in applicationUserDB
                                                         select new SelectListItem
                                                         {
                                                             Text = x.FirstName + " " + x.LastName + " " + x.Email,
                                                             Value = x.Id.ToString()
                                                         }).ToList();
                ViewBag.applicationUsers = applicationUsers;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequestDto model)
        {
            try
            {
                var result = await _orderService.CreateAsync(model);
                if (result is not null)
                    return RedirectToAction("GetAll");
                return View(model);

            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return View(model);
            }
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(Guid Id)
        {
            try
            {
                var productDB = await _orderService.GetAllActivatedProduct();
                if (productDB == null)
                    return View("Index");
                List<SelectListItem> products = (from x in productDB
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.Id.ToString()
                                                 }).ToList();
                ViewBag.products = products;

                var applicationUserDB = await _orderService.GetAllApplicationUser();
                if (applicationUserDB == null)
                    return View("Index");
                List<SelectListItem> applicationUsers = (from x in applicationUserDB
                                                         select new SelectListItem
                                                         {
                                                             Text = x.FirstName + " " + x.LastName + " " + x.Email,
                                                             Value = x.Id.ToString()
                                                         }).ToList();
                ViewBag.applicationUsers = applicationUsers;

                var result = await _orderService.GetByIdAsync(Id);
                if (result == null)
                    return RedirectToAction("Error", "Home");
                var viewModel = _autoMapper.Map<UpdateOrderRequestDto>(result);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Update(UpdateOrderRequestDto model)
        {
            try
            {
                var result = await _orderService.UpdateAsync(model);
                if (result.Status == false)
                    return RedirectToAction("Error", "Home");

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return View(model);
            }
        }
        #endregion

        #region GetById
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var result = await _orderService.GetByIdAsync(Id);
                if (result == null)
                    return RedirectToAction("Error", "Home");
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return View();
            }
        }
        #endregion

        #region Activate
        public async Task<IActionResult> Activate(Guid Id)
        {
            try
            {
                var result = await _orderService.ActivateAsync(Id);
                if (result == null)
                    return RedirectToAction("Error", "Home");
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return View(); /////////////////////////////////////
            }
        }
        #endregion

        #region UnActivate
        public async Task<IActionResult> UnActivate(Guid Id)
        {
            try
            {
                var result = await _orderService.UnActivateAsync(Id);
                if (result == null)
                    return NotFound();
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion
    }
}

