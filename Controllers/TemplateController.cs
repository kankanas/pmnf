using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VidsNet.Classes;
using VidsNet.DataModels;
using VidsNet.Enums;
using VidsNet.Filters;
using VidsNet.Models;
using VidsNet.ViewModels;

namespace VidsNet.Controllers
{
    [Authorize]
    [TypeFilter(typeof(ExceptionFilter))]
    public class TemplateController : BaseController
    {
        private DatabaseContext _db;
        public TemplateController(DatabaseContext db, UserData userData)
        : base(userData)
        {
            _db = db;
        }

        public IActionResult Move(int id)
        {
            if (ModelState.IsValid)
            {
                var currentItem = _db.VirtualItems.Where(x => x.Id == id).FirstOrDefault();
                var currentItemId = 0;
                if (currentItem is VirtualItem)
                {
                    currentItemId = currentItem.ParentId;
                }
                var viewModel = new MoveViewModel(_user)
                {
                    Items = _db.VirtualItems.Where(x => x.UserId == _user.Id && !x.IsDeleted && !x.IsViewed && x.Type == Item.Folder).ToList(),
                    CurrentItem = currentItemId
                };
                return View(viewModel);
            }
            return NotFound();
        }

        public IActionResult VirtualItems()
        {
            if (ModelState.IsValid)
            {
                var virtualItems = _db.VirtualItems.Where(x => x.UserId == _user.Id).OrderBy(x => x.Type).ThenBy(y => y.Name).ToList();
                var realItems = _db.RealItems.ToList();
                var model = new HomeViewModel(_user) { VirtualItems = virtualItems, RealItems = realItems };
                return View(model);
            }
            return NotFound();
        }

        public IActionResult ViewedItems()
        {
            if (ModelState.IsValid)
            {
                var virtualItems = _db.VirtualItems.Where(x => x.UserId == _user.Id && x.IsViewed).OrderBy(x => x.Type).ThenBy(y => y.Name).ToList();
                var realItems = _db.RealItems.ToList();
                var model = new HomeViewModel(_user) { VirtualItems = virtualItems, RealItems = realItems };
                return View(model);
            }
            return NotFound();
        }

        public IActionResult DeletedItems()
        {
            if (ModelState.IsValid)
            {
                var virtualItems = _db.VirtualItems.Where(x => x.UserId == _user.Id && x.IsDeleted).OrderBy(x => x.Type).ThenBy(y => y.Name).ToList();
                var realItems = _db.RealItems.ToList();
                var model = new HomeViewModel(_user) { VirtualItems = virtualItems, RealItems = realItems };
                return View(model);
            }
            return NotFound();
        }

        public IActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                var item = _db.VirtualItems.Where(x => x.UserId == _user.Id && x.Id == id).FirstOrDefault();
                if (item is VirtualItem)
                {
                    var model = new EditViewModel(_user) { Value = item.Name };
                    return View(model);
                }
            }

            return NotFound();
        }

        public IActionResult Create()
        {
            if (ModelState.IsValid)
            {
                var viewModel = new MoveViewModel(_user)
                {
                    Items = _db.VirtualItems.Where(x => x.UserId == _user.Id && !x.IsDeleted && !x.IsViewed && x.Type == Item.Folder).ToList(),
                    CurrentItem = 0
                };
                return View(viewModel);
            }

            return NotFound();
        }

        public IActionResult SMBadge(int id)
        {
            ViewBag.Count = id;
            return View("Badge");
        }

        public IActionResult ImportantSystemMessages()
        {
            var messages = _db.SystemMessages.Where(x => x.UserId == _user.Id && x.Severity >= Severity.Info).OrderByDescending(y => y.Timestamp).ToList();
            var model = new SystemMessagesViewModel(_user) { Messages = messages };
            return PartialView("SystemMessagesList", model);
        }

        public IActionResult AllSystemMessages()
        {
            var messages = _db.SystemMessages.Where(x => x.UserId == _user.Id).OrderByDescending(y => y.Timestamp).ToList();
            var model = new SystemMessagesViewModel(_user) { Messages = messages, ListingType = 1 };
            return PartialView("SystemMessagesList", model);
        }
    }
}