using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Services;
using WebUI.Models.CatalogVM;
using WebUI.Services.Interfaces;

namespace WebUI.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
        }


        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput model)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }
            model.UserId = _sharedIdentityService.GetUserId;
            await _catalogService.CreateCourseAsync(model);
            return RedirectToAction("Index", "Courses");
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByIdAsync(id);
            var categories = await _catalogService.GetAllCategoryAsync();
            if (course == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", course.Id);
            CourseUpdateInput model = new CourseUpdateInput()
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Description = course.Description,
                Feature = course.Feature,
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Picture = course.Picture
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput model)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", model.Id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _catalogService.UpdateCourseAsync(model);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
