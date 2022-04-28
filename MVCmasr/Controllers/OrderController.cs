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


        [HttpGet]
        public IActionResult Cart()
		{
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            List<OrderItemsSession> orders = context.OrderItemsSessions.Where(i => i.UserId == userId).ToList();

            UpdateQuantities();

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


        [HttpGet("cart")]
        //[Route("{id:int}")]
        public IActionResult ChangeQuantity(int albumId, int quantity = 1 , int oldQuantity = 0)
        {
            List<OrderItemsSession> albumOrders = context.OrderItemsSessions.Where(i => i.AlbumId == albumId).ToList();
            context.OrderItemsSessions.RemoveRange(albumOrders);

            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;

            OrderItemsSession order = new OrderItemsSession();
            order.AlbumId = albumId;
            order.UserId = userId;

            order.Quantity = quantity;
            //order.Quantity = quantity - oldQuantity;

            Album album = unitofwork.AlbumRepository.GetById(albumId);
            order.Price = album.Price * order.Quantity;

            context.OrderItemsSessions.Add(order);
            context.SaveChanges();

            //UpdateQuantities(quantity);
            //UpdateQuantities();

            SetData();

            //return RedirectToAction("UpdateQuantities", "Order", new {_quantity= quantity});
            //return RedirectToAction("Cart", "Order");
            //return View("Cart", orders);
            return Ok();
        }


		public IActionResult UpdateQuantities() // int _quantity
        {
            SetData();

			//var albumsIds = orders.Select(a => a.AlbumId).Distinct().ToList();

			//HttpContext.Session.SetInt32("Quantity", albums.Count());

            //return Content(_quantity.ToString());
			return Ok();
		}


        // GET: OrderController/RemoveItem/5
        public ActionResult RemoveItem(int albumId)
        {
            List<OrderItemsSession> albumOrders = context.OrderItemsSessions.Where(i => i.AlbumId == albumId).ToList();
            context.OrderItemsSessions.RemoveRange(albumOrders);
            context.SaveChanges();

            SetData();

            return Ok();
        }


        [HttpGet]
        public IActionResult Checkout()
		{
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            List<OrderItemsSession> orders = context.OrderItemsSessions.Where(i => i.UserId == userId).ToList();

            UpdateQuantities();

            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.User = user;

            return View(orders);
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



        private void SetData()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;

            List<OrderItemsSession> orders = context.OrderItemsSessions.Where(i => i.UserId == userId).ToList();

            var q = from albumm in orders.OrderBy(a => a.AlbumId).GroupBy(a => a.AlbumId)
                    select new
                    {
                        count = albumm.Sum(s => s.Quantity),
                        albumm.First().AlbumId
                    };
            var quantities = q.Select(s => s.count).ToList();

            var selectedAlbumIds = orders.Select(i => i.AlbumId).ToList();

            var albums = unitofwork.AlbumRepository.GetAllWithAllData().Where(a => selectedAlbumIds.Contains(a.Id)).ToList();

            List<decimal> prices = new List<decimal>();
            for (var i = 0; i < quantities.Count; i++)
            {
                prices.Add(quantities[i] * albums[i].Price);
            };

            ViewBag.Albums = albums;
            ViewBag.Quantities = quantities;
            ViewBag.Prices = prices;

            ViewBag.TotalPrice = orders.Sum(s => s.Price);
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
