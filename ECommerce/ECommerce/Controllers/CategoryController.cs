using BussinessLogicLayer.Dtos.CategoryDtos;
using BussinessLogicLayer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
                var result = await _categoryService.GetAllActivatedAsync();
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
                var result = await _categoryService.GetAllAsync();
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
                var result = await _categoryService.GetAllUnActivatedAsync();
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
                var result = await _categoryService.DeleteAsync(Id);
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
        public IActionResult Create()
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
        public async Task<IActionResult> Create(CreateCategoryRequestDto model)
        {
            try
            {
                var result = await _categoryService.CreateAsync(model);
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
                var result = await _categoryService.GetByIdAsync(Id);
                if (result == null)
                    return RedirectToAction("Error", "Home");
                var viewModel = new UpdateCategoryRequestDto
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    IsActive = result.IsActive
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryRequestDto model)
        {
            try
            {
                var result = await _categoryService.UpdateAsync(model);
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
                var result = await _categoryService.GetByIdAsync(Id);
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
                var result = await _categoryService.ActivateAsync(Id);
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
                var result = await _categoryService.UnActivateAsync(Id);
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
