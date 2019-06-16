using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CategoriesCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly IGetCategoriesCommand _getCommand;
        private readonly IGetCategoryCommand _getOneCommand;
        private readonly IAddCategoryCommand _addCommand;
        private readonly IEditCategoryCommand _editCommand;
        private readonly IDeleteCategoryCommand _deleteCommand;
        private readonly IGetAllCategoriesCommand _getAllCommand;

        public CategoriesController(IGetCategoriesCommand getCommand, IGetCategoryCommand getOneCommand, IAddCategoryCommand addCommand, IEditCategoryCommand editCommand, IDeleteCategoryCommand deleteCommand, IGetAllCategoriesCommand getAllCommand)
        {
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
            _getAllCommand = getAllCommand;
        }



        // GET: Categories
        public ActionResult Index(CategorySearch search)
        {
            var result = _getAllCommand.Execute(search);
            return View(result);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {

            try
            {
                var category = _getOneCommand.Execute(id);
                return View(category);
            }
            catch (Exception)
            {
                return View();
            }

        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryDto dto)
        {

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                _addCommand.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Category already exists";
            }
            catch (Exception)
            {
                TempData["error"] = "Error";
            }

            return View();
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var dto = _getOneCommand.Execute(id);
                return View(dto);
            }
            catch (Exception)
            {

                return RedirectToAction("index");
            }
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                _editCommand.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Category already exists.";
                return View(dto);
            }
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var category = _getOneCommand.Execute(id);
                return View(category);
            }
            catch (Exception)
            {

                return RedirectToAction("index");
            }
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategoryDto dto)
        {
            try
            {
                _deleteCommand.Execute(id);
                return RedirectToAction("Index");
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "Object doesn't exist.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "Error";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}