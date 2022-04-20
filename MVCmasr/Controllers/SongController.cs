using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCmasr.Data.UnitOfWork;
using MVCmasr.Models;
using MVCmasr.ViewModels;

namespace MVCmasr.Controllers
{
    public class SongController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyfService;

        public SongController( IUnitOfWork unitOfWork , 
                                IWebHostEnvironment webHostEnvironment , 
                                INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            List<Song> songs = _unitOfWork.SongRepository.GetAll();
            return View(songs);
        }

        public IActionResult Details(int id)
        {
            Song song = _unitOfWork.SongRepository.GetById(id);
            return View(song);
        }

        [HttpGet]
        public IActionResult New()
        {
            var SongVm = new SongGenreAlbumArtistViewModel();
            Song song = new Song();
            List<Genre> genres = _unitOfWork.GenreRepository.GetAll();
            List<Artist> artists = _unitOfWork.ArtistRepository.GetAll();
            List<Album> albums = _unitOfWork.AlbumRepository.GetAll();
            SongVm.Song = song;
            SongVm.Albums = albums;
            SongVm.Genres = genres;
            SongVm.Artists = artists;
            return View(SongVm);
        }

        [HttpPost]
        public IActionResult SaveNew(SongGenreAlbumArtistViewModel SongVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    InsertAudio(SongVm.Song);

                    var newSongArtists = _unitOfWork.ArtistRepository.GetAll()
                        .Where(s => SongVm.SelectedArtistsIds
                        .Contains(s.Id)).ToList();

                    SongVm.Song.Artists= newSongArtists;

                    var newSongGenre = _unitOfWork.GenreRepository.GetById(SongVm.SelectedGenreId);
                    var newSongAlbum = _unitOfWork.AlbumRepository.GetById(SongVm.SelectedAlbumId);

                    SongVm.Song.Genre = newSongGenre;
                    SongVm.Song.Album = newSongAlbum;

                    _unitOfWork.SongRepository.Insert(SongVm.Song);

                    _notyfService.Success("Song Successfully Created");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _notyfService.Error(ex.Message);
            }
            _notyfService.Error("There is an Error in Song Data");

            List<Genre> genres = _unitOfWork.GenreRepository.GetAll();
            List<Artist> artists = _unitOfWork.ArtistRepository.GetAll();
            List<Album> albums = _unitOfWork.AlbumRepository.GetAll();

            SongVm.Albums = albums;
            SongVm.Genres = genres;
            SongVm.Artists = artists;
            return View("New", SongVm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var SongVm = new SongGenreAlbumArtistViewModel();
            
            var song = _unitOfWork.SongRepository.GetByIdWithAllData(id);
            SongVm.Song = song;

            var oldSongArtists = song.Artists;
            SongVm.SelectedArtistsIds = oldSongArtists.Select(a => a.Id).ToList();
            SongVm.SelectedGenreId = song.GenreId;
            SongVm.SelectedAlbumId = song.AlbumId;

            List<Genre> genres = _unitOfWork.GenreRepository.GetAll();
            List<Artist> artists = _unitOfWork.ArtistRepository.GetAll();
            List<Album> albums = _unitOfWork.AlbumRepository.GetAll();

            SongVm.Albums = albums;
            SongVm.Genres = genres;
            SongVm.Artists = artists;

            return View(SongVm);
        }

        [HttpPost]
        public IActionResult SaveEdit(SongGenreAlbumArtistViewModel SongVm)
        {
            if (ModelState.IsValid)
            {
                Song oldSong= _unitOfWork.SongRepository.GetById(SongVm.Song.Id);
                if (SongVm.Song.AudioFile is not null)
                {
                    DeleteAudio(oldSong);
                    InsertAudio(SongVm.Song);

                }

                var newSongArtists = _unitOfWork.ArtistRepository.GetAll()
                        .Where(a => SongVm.SelectedArtistsIds
                        .Contains(a.Id)).ToList();

                SongVm.Song.Artists = newSongArtists;

                _unitOfWork.SongRepository.Update(SongVm.Song.Id, SongVm.Song);
                _notyfService.Success("Song Successfully Edited");
                return RedirectToAction("Index");
            }

            _notyfService.Error("There is an Error in Song Data");
            List<Genre> genres = _unitOfWork.GenreRepository.GetAll();
            List<Artist> artists = _unitOfWork.ArtistRepository.GetAll();
            List<Album> albums = _unitOfWork.AlbumRepository.GetAll();

            SongVm.Albums = albums;
            SongVm.Genres = genres;
            SongVm.Artists = artists;

            return View("Edit", SongVm);
        }

        public IActionResult Delete(int id)
        {
            DeleteAudio(_unitOfWork.SongRepository.GetById(id));
            _unitOfWork.SongRepository.Delete(id);
            _notyfService.Success("Song Successfully Deleted");
            return RedirectToAction("Index");
        }


        public void InsertAudio(Song song)
        {
            string songsFolderPath = @$"{_webHostEnvironment.WebRootPath}\assets\audios\song\";
            var songName = String.Concat(Path.GetFileName(song.AudioFile.FileName).Where(c => !(Char.IsWhiteSpace(c))));
            string fileName = Path.Combine(songsFolderPath, songName);
            song.AudioFile.CopyTo(new FileStream(fileName, FileMode.Create));
            song.Audio = Path.Combine($@"/assets/audios/song/{songName}");
        }

        public void DeleteAudio(Song song)
        {
            string songPath = $"{_webHostEnvironment.WebRootPath}{song.Audio}";
            System.IO.File.Delete(songPath);
        }

    }
}
