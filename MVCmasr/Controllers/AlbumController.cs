using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MVCmasr.Data.UnitOfWork;
using MVCmasr.Models;
using MVCmasr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCmasr.Controllers
{
	public class AlbumController : Controller
	{
		
		INotyfService notyfService;

		private readonly IUnitOfWork unitOfWork;

        public AlbumController(IUnitOfWork _unitOfWork, INotyfService _notyfService)
		{
            unitOfWork = _unitOfWork;
            notyfService = _notyfService;
		}

		public IActionResult Index()
		{
			List<Album> albums = unitOfWork.AlbumRepository.GetAll();
			return View(albums);
		}

		public IActionResult Details(int id)
		{
			Album album = unitOfWork.AlbumRepository.GetById(id);
			return View(album);
		}

		[HttpGet]
		public IActionResult New()
		{
			var AlbumGenreVM = new AlbumGenreViewModel();
            Album album = new Album();
			List<Genre> genres = unitOfWork.GenreRepository.GetAll();
			List<Artist> artists = unitOfWork.ArtistRepository.GetAll();
			AlbumGenreVM.Album = album;
			AlbumGenreVM.Genres = genres;
            AlbumGenreVM.Artists = artists;
			return View(AlbumGenreVM);
		}

        [HttpPost]
        public IActionResult SaveNew(AlbumGenreViewModel albumGenreVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newAlbumGenres = unitOfWork.GenreRepository.GetAll()
                        .Where(g=> albumGenreVM.SelectedGenresIds
                        .Contains(g.Id)).ToList();

                    albumGenreVM.Album.Genres = newAlbumGenres;
                    
                    var newAlbumArtists = unitOfWork.ArtistRepository.GetAll()
                        .Where(a=> albumGenreVM.SelectedArtistsIds
                        .Contains(a.Id)).ToList();

                    albumGenreVM.Album.Genres = newAlbumGenres;
                    albumGenreVM.Album.Artists = newAlbumArtists;
                    unitOfWork.AlbumRepository.Insert(albumGenreVM.Album);
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
            return View("New", albumGenreVM);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var AlbumGenreVM = new AlbumGenreViewModel();
            var album = unitOfWork.AlbumRepository.GetByIdWithAllData(id);
            AlbumGenreVM.Album = album;
            List<Genre> genres = unitOfWork.GenreRepository.GetAll();
            List<Artist> artists = unitOfWork.ArtistRepository.GetAll();
            AlbumGenreVM.Genres = genres;
            AlbumGenreVM.Artists = artists;

            var oldAlbumGenres = album.Genres;
            var oldAlbumArtists = album.Artists;
           
            AlbumGenreVM.SelectedGenresIds = oldAlbumGenres.Select(g=> g.Id).ToList();
            AlbumGenreVM.SelectedArtistsIds = oldAlbumArtists.Select(a=> a.Id).ToList();

            return View(AlbumGenreVM);
        }

        [HttpPost]
        public IActionResult SaveEdit(AlbumGenreViewModel albumGenreVM)
        {
            //Album oldAlbum = unitOfWork.AlbumRepository.GetById(id);
            if (ModelState.IsValid)
            {
                var newAlbumGenres = unitOfWork.GenreRepository.GetAll()
                        .Where(g => albumGenreVM.SelectedGenresIds
                        .Contains(g.Id)).ToList();
                
                var newAlbumArtists = unitOfWork.ArtistRepository.GetAll()
                        .Where(a => albumGenreVM.SelectedArtistsIds
                        .Contains(a.Id)).ToList();

                albumGenreVM.Album.Genres = newAlbumGenres;
                albumGenreVM.Album.Artists = newAlbumArtists;

                unitOfWork.AlbumRepository.Update(albumGenreVM.Album.Id,albumGenreVM.Album);
                notyfService.Success("Album Successfully Edited");
                return RedirectToAction("Index");
            }
            notyfService.Error("There is an Error in Album Data");
            return View("Edit", albumGenreVM);
        }

        //public IActionResult Delete(int id)
        //{
        //	albumRepository.Delete(id);
        //	notyfService.Success("Album Successfully Deleted");
        //	return RedirectToAction("Index");
        //}

    }
}
