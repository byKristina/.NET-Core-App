using System;
using System.IO;
using System.Linq;
using Application.Commands.PostsCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Helpers;
using Application.Searches;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;

namespace WebApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogContext Context;
        private readonly IGetPostsCommand _getCommand;
        private readonly IGetPostCommand _getOneCommand;
        private readonly IAddPostCommand _addCommand;
        private readonly IEditPostCommand _editCommand;
        private readonly IDeletePostCommand _deleteCommand;

        public PostsController(BlogContext context, IGetPostsCommand getCommand, IGetPostCommand getOneCommand, IAddPostCommand addCommand, IEditPostCommand editCommand, IDeletePostCommand deleteCommand)
        {
            Context = context;
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
        }


        // GET: Posts
        public ActionResult Index([FromQuery] PostSearch search)
        {
            try
            {
                var posts = _getCommand.Execute(search);
                return View(posts.Data);
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "Server error has occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var post = _getOneCommand.Execute(id);
                return View(post);
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "Server error has occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.Categories = Context.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name 
                });
                ViewBag.Users = Context.Users.Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                });
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] PostImageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var extension = Path.GetExtension(dto.Image.FileName);

            if (!FileUpload.AllowedExtensions.Contains(extension))
            {
                return UnprocessableEntity("You must upload image.");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

                dto.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                var post = new PostDto
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Content = dto.Content,
                    ImagePath = newFileName,
                    CategoryId = dto.CategoryId,
                    UserId = dto.UserId
                };

                _addCommand.Execute(post);

                TempData["success"] = "Post successfully added.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
               
                TempData["error"] = "An error has occured.";
            }

            return View();


        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = Context.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
            ViewBag.Users = Context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            });

            return View();
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] PostImageDto dto)
        {
            dto.Id = id;

            if (!ModelState.IsValid)
            {
                return View();
            }

            var extension = Path.GetExtension(dto.Image.FileName);

            if (!FileUpload.AllowedExtensions.Contains(extension))
            {
                return UnprocessableEntity("You must upload image.");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

                dto.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                var post = new PostDto
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Content = dto.Content,
                    ImagePath = newFileName,
                    CategoryId = dto.CategoryId,
                    UserId = dto.UserId
                };

                _editCommand.Execute(post);

                TempData["success"] = "Post successfully edited.";
                 return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                TempData["error"] = "An error has occured.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var post = _getOneCommand.Execute(id);
                return View(post);
            }
            catch (Exception)
            {

                return RedirectToAction("index");
            }
        }

        // POST: Posts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, PostDto dto)
        {
            try
            {
                _deleteCommand.Execute(id);
                return RedirectToAction("Index");
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "Post doesn't exist.";
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