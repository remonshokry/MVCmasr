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

            var q = from album in orders.GroupBy(a => a.AlbumId)
                             select new
                             {
                                 count = album.Sum(s => s.Quantity),
                                 album.First().AlbumId
                             };
            var quantities = q.Select(s => s.count).ToList();

            //var prices = orders.GroupBy(a => a.AlbumId).Select(g => g.Key);

            var selectedAlbumIds = orders.Select(i => i.AlbumId).ToList();
            var albumsIds = orders.Select(a => a.AlbumId).Distinct().ToList();



            var albums = unitofwork.AlbumRepository.GetAllWithAllData().Where(a => selectedAlbumIds.Contains(a.Id)).ToList();
            var quantitiesss = orders.GroupBy(a => a.AlbumId).Select(g => g.Key).ToList();


            List<decimal> prices = new List<decimal>();
            for (var i=0; i < quantities.Count; i++)
			{
                prices.Add(quantities[i] * albums[i].Price);
			};

            HttpContext.Session.SetInt32("Quantity", albums.Count());


            //orders = orders.Select(o => o.Album = unitofwork.AlbumRepository.GetById(o.AlbumId)).ToList();


            ViewBag.Albums = albums;
            ViewBag.Quantities = quantities;
            ViewBag.Prices = prices;

            ViewBag.TotalPrice = orders.Sum(s => s.Price);

            return View(orders);
		}

        // GET: OrderController/AddToCart
        [HttpGet]
        [Route("id:int")]
        public ActionResult AddToCart(int id)
        {
            //return RedirectToAction("Cart", "Order");

            ChangeQuantity(id);

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


        [HttpGet("cart")]
        //[Route("{id:int}")]
        public IActionResult ChangeQuantity(int albumId, int quantity = 1 , int oldQuantity = 0)
        {
            OrderItemsSession order = new OrderItemsSession();
            order.AlbumId = albumId;
            order.UserId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;

            //order.Quantity = quantity;
            order.Quantity = quantity - oldQuantity;

            Album album = unitofwork.AlbumRepository.GetById(albumId);
            order.Price = album.Price * order.Quantity;
            context.OrderItemsSessions.Add(order);
            context.SaveChanges();

            return Ok();
        }




        //      public IActionResult ChangeQuantity(int quantity, int albumId)
        //{
        // add this code to proceed to checkout

        //Album album = unitofwork.AlbumRepository.GetById(albumId);
        //OrderItem item = new OrderItem();
        //item.Quantity = quantity;
        //item.AlbumId = albumId;
        //item.Price = album.Price * quantity;
        //item.OrderId = 0;

        //context.OrderItems.Add(item);
        //context.SaveChanges();

        //          ViewBag.Quantity = quantity;
        //          return Content(quantity.ToString());
        //          //return RedirectToAction("Cart", "Order");
        //}

    }
}
