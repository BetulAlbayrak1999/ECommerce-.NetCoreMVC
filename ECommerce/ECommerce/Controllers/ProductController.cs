using AutoMapper;
using BussinessLogicLayer.Dtos.ProductDtos;
using BussinessLogicLayer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _autoMapper;
        public ProductController(IProductService ProductService, IMapper autoMapper)
        {
            _productService = ProductService;
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
                var result = await _productService.GetAllActivatedAsync();
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
                var result = await _productService.GetAllAsync();
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
                var result = await _productService.GetAllUnActivatedAsync();
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
                var result = await _productService.DeleteAsync(Id);
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
        public async Task<IActionResult> Create()
        {
            try
            {
                var categoryDB = await _productService.GetAllActivatedCategory();
                if (categoryDB == null)
                    return View("Index");
                List<SelectListItem> categories = (from x in categoryDB
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();
                ViewBag.categories = categories;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequestDto model)
        {
            try
            {
                var result = await _productService.CreateAsync(model);
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
                var result = await _productService.GetByIdAsync(Id);
                if (result == null)
                    return RedirectToAction("Error", "Home");
                var viewModel = _autoMapper.Map<UpdateProductRequestDto>(result);

                var categoryDB = await _productService.GetAllActivatedCategory();
                if (categoryDB == null)
                    return View("Index");
                List<SelectListItem> categories = (from x in categoryDB
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
                ViewBag.categories = categories;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
      
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductRequestDto model)
        {
            try
            {
                var result = await _productService.UpdateAsync(model);
                if (result.Status == false)
                    return RedirectToAction("Error", "Home");

                return RedirectToAction("Index");
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
                var result = await _productService.GetByIdAsync(Id);
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
                var result = await _productService.ActivateAsync(Id);
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
                var result = await _productService.UnActivateAsync(Id);
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

