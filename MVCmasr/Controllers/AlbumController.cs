using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCmasr.Data.UnitOfWork;
using MVCmasr.Models;
using MVCmasr.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MVCmasr.Controllers
{
    //[Authorize]
	public class AlbumController : Controller
	{
		
		INotyfService notyfService;

		private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment hostEnvironment;

        public AlbumController(IUnitOfWork _unitOfWork, 
                                INotyfService _notyfService, 
                                IWebHostEnvironment _hostEnvironment)
        { 
            hostEnvironment = _hostEnvironment;
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
                    InsertImage(albumGenreVM.Album);

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
            List<Genre> genres = unitOfWork.GenreRepository.GetAll();
            List<Artist> artists = unitOfWork.ArtistRepository.GetAll();
            albumGenreVM.Genres = genres;
            albumGenreVM.Artists = artists;
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
            if (ModelState.IsValid)
            {
            Album oldAlbum = unitOfWork.AlbumRepository.GetById(albumGenreVM.Album.Id);
                if(albumGenreVM.Album.ImageFile is not null)
                {
                    DeleteImage(oldAlbum);
                    InsertImage(albumGenreVM.Album);
                    
                }

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

        public IActionResult Delete(int id)
        {
            DeleteImage(unitOfWork.AlbumRepository.GetById(id));
            unitOfWork.AlbumRepository.Delete(id);
            notyfService.Success("Album Successfully Deleted");
            return RedirectToAction("Index");
        }

        public void InsertImage(Album album)
        {
            string imagesFolderPath = hostEnvironment.WebRootPath + @"\assets\img\albumCovers";
            string newDirectory = $"{album.Title}-{DateTime.Now.Ticks}";
            Directory.CreateDirectory(Path.Combine(imagesFolderPath, newDirectory));
            string fileName = Path.Combine(imagesFolderPath, newDirectory, Path.GetFileName(album.ImageFile.FileName));
            album.ImageFile.CopyTo(new FileStream(fileName, FileMode.Create));
            album.Image = Path.Combine($@"/assets/img/albumCovers/{newDirectory}/{Path.GetFileName(album.ImageFile.FileName)}");
        }

        public void DeleteImage(Album album)
        {
            string wwwrootPath = hostEnvironment.WebRootPath;
            string rootPath = wwwrootPath + @"\assets\img\albumCovers";
            var array = album.Image.Split("/").SkipLast(1);
            string albumPath = wwwrootPath + String.Join("/", array);
            string imagePath = wwwrootPath + album.Image;
            //using (var stream = System.IO.File.Open(imagePath, FileMode.Open, FileAccess.Write))
            //{
                //stream.Close();
                //stream.Dispose();
                if (Directory.Exists(albumPath))
                {
                    Directory.Delete(albumPath, true);

                }
            //}
        }

        [HttpGet]
        public IActionResult AlbumsDetails()
        {
            List<Album> albums = unitOfWork.AlbumRepository.GetAll();
            return View(albums);
        }
    }

}
