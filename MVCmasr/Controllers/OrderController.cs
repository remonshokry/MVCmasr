using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCmasr.Context;
using MVCmasr.Data.UnitOfWork;
using MVCmasr.Models;
using MVCmasr.ViewModels.Order;
using System.Collections.Generic;
using System.Linq;

namespace MVCmasr.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitofwork;
        private readonly INotyfService notyfService;
		private readonly ApplicationDbContext context;

		public OrderController(IUnitOfWork _unitofwork, INotyfService _notyfService, ApplicationDbContext _context)
        {
            unitofwork = _unitofwork;
            notyfService = _notyfService;
			context = _context;
		}

        public ActionResult Index()
        {
            return View("AddToCart");
            //return Content("cart");
        }

        [HttpGet]
        public IActionResult Cart()
		{
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            List<OrderItemsSession> orders = context.OrderItemsSessions.Where(i => i.UserId == userId).ToList();
            var quantities = from album in orders.GroupBy(a => a.AlbumId)
                             select new
                             {
                                 count = album.Count(),
                                 album.First().AlbumId
                             };

            var selectedAlbumIds = orders.Select(i => i.AlbumId).ToList();
            var albumsIds = orders.Select(a => a.AlbumId).Distinct().ToList();



            var albums = unitofwork.AlbumRepository.GetAllWithAllData().Where(a => selectedAlbumIds.Contains(a.Id)).ToList();
            var quantitiesss = orders.GroupBy(a => a.AlbumId).Select(g => g.Key).ToList();





            //orders = orders.Select(o => o.Album = unitofwork.AlbumRepository.GetById(o.AlbumId)).ToList();


            ViewBag.Albums = albums;
            ViewBag.Quantities = quantities;

            return View(orders);
		}

        // GET: OrderController/AddToCart
        [HttpGet]
        [Route("id:int")]
        public ActionResult AddToCart(int id)
        {
            //return RedirectToAction("Cart", "Order");
            OrderItemsSession order = new OrderItemsSession();
            order.AlbumId = id;
            order.UserId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            order.Quantity = 5;
            Album album = unitofwork.AlbumRepository.GetById(id);
            order.Price = album.Price * order.Quantity;
            context.OrderItemsSessions.Add(order);
            context.SaveChanges();
            notyfService.Success("Album Added to Cart Successfully");
            //return RedirectToAction("closePreview", "Order");
            //return RedirectToAction("Details", "Album", new { id = id });
            return RedirectToAction("Index", "Album");
        }


        [HttpGet]
        public IActionResult closePreview()
		{
            return View();
		}

        // POST: OrderController/UpdateCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCart(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(AddToCart));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(AddToCart));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(AddToCart));
            }
            catch
            {
                return View();
            }
        }
    }
}
