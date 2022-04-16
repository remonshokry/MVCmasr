using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCmasr.Context;
using MVCmasr.Models;
using MVCmasr.Repository;
using MVCmasr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MVCmasr.Controllers
{
	public class AlbumController : Controller
	{
		IAlbumRepository albumRepository;
		INotyfService notyfService;

		// ems7 elaraf da
		ApplicationDbContext context;

		public AlbumController(IAlbumRepository _albumRepository, INotyfService _notyfService, ApplicationDbContext _context)
		{
			albumRepository = _albumRepository;
			notyfService = _notyfService;
			context = _context;
		}
		public IActionResult Index()
		{
			List<Album> albums = albumRepository.GetAll();
			return View(albums);
		}

		public IActionResult Details(int id)
		{
			Album album = albumRepository.GetById(id);
			return View(album);
		}

		[HttpGet]
		public IActionResult New()
		{
			var AlbumGenreVM = new AlbumGenreViewModel();
			//Album album = new Album();
			List<Genre> genres2 = context.Genre.ToList();
			List<SelectListItem> genres = context.Genre.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
			AlbumGenreVM.Genre = genres;
			AlbumGenreVM.Album = new Album();
			ViewBag.Genres = genres2;
			return View(AlbumGenreVM);
		}

		[HttpPost]
		public IActionResult SaveNew(AlbumGenreViewModel albumGenre)
		{
			try
			{
				if (ModelState.IsValid)
				{
					//var x = JsonSerializer.Serialize(album.AlbumGenre);
					var x = JsonSerializer.Serialize(albumGenre.Album.AlbumGenre);
					albumGenre.Album.AlbumGenre = (ICollection<AlbumGenre>)albumGenre.Genre;
					albumRepository.Insert(albumGenre.Album);
					notyfService.Success("Album Successfully Created");
					//notyfService.Custom("Custom Notification...", 10, "#B500FF", "fa fa-home");
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				notyfService.Error(ex.Message);
			}
			notyfService.Error("There is an Error in Album Data");
			return View("New", albumGenre);
		}

		public IActionResult Edit(int id)
		{
			var album = albumRepository.GetById(id);
			return View(album);
		}

		[HttpPost]
		public IActionResult SaveEdit(int id, Album newAlbum)
		{
			Album oldAlbum = albumRepository.GetById(id);
			if (ModelState.IsValid)
			{
				albumRepository.Update(id, newAlbum);
				notyfService.Success("Album Successfully Edited");
				return RedirectToAction("Index");
			}
			notyfService.Error("There is an Error in Album Data");
			return View("Edit", newAlbum);
		}

		public IActionResult Delete(int id)
		{
			albumRepository.Delete(id);
			notyfService.Success("Album Successfully Deleted");
			return RedirectToAction("Index");
		}

	}
}
