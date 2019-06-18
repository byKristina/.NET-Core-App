using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CommentsCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CommentsController : Controller
    {
        private readonly BlogContext Context;
        private readonly IGetCommentsCommand _getCommand;
        private readonly IGetCommentCommand _getOneCommand;
        private readonly IAddCommentCommand _addCommand;
        private readonly IEditCommentCommand _editCommand;
        private readonly IDeleteCommentCommand _deleteCommand;
        private readonly IGetAllCommentsCommand _getAllCommand;

        public CommentsController(BlogContext context, IGetCommentsCommand getCommand, IGetCommentCommand getOneCommand, IAddCommentCommand addCommand, IEditCommentCommand editCommand, IDeleteCommentCommand deleteCommand, IGetAllCommentsCommand getAllCommand)
        {
            Context = context;
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
            _getAllCommand = getAllCommand;
        }



        // GET: Comments
        public ActionResult Index(CommentSearch search)
        {
            var result = _getAllCommand.Execute(search);
            return View(result);
        }

        // GET: Comments/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var comment = _getOneCommand.Execute(id);
                return View(comment);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.Posts = Context.Posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title
            });

            ViewBag.Users = Context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username
            });

            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentDto dto)
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
           
            catch (Exception)
            {
                TempData["error"] = "Error";
            }

            return View();
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Posts = Context.Posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title
            });

            ViewBag.Users = Context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username
            });

            return View();
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CommentDto dto)
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
            catch (Exception)
            {
                TempData["error"] = "Error";
                return View(dto);
            }
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var comment = _getOneCommand.Execute(id);
                return View(comment);
            }
            catch (Exception)
            {

                return RedirectToAction("index");
            }
        }

        // POST: Comments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CommentDto dto)
        {
            try
            {
                _deleteCommand.Execute(id);
                return RedirectToAction("Index");
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "Comment doesn't exist.";
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