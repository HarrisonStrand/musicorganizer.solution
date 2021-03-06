using Microsoft.AspNetCore.Mvc;
using MusicOrganizer.Models;
using System.Collections.Generic;

namespace MusicOrganizer.Controllers
{
  public class ArtistsController : Controller
  { 
    [HttpGet("/artists")]
    public ActionResult Index()
    {
      List<Artist> allArtists = Artist.GetAll(); 
      return View(allArtists); 
    }

    [HttpGet("/artists/new")]
    public ActionResult New()
    {
      return View(); 
    }

    [HttpPost("/artists")]
    public ActionResult Create(string artistName)
    {
      Artist newArtist = new Artist(artistName);
      return RedirectToAction("Index");
    }

    [HttpGet("/artists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>(); 
      Artist selectedArtist = Artist.Find(id);
      List<Album> artistAlbums = selectedArtist.Albums; 
      model.Add("artist", selectedArtist);
      model.Add("albums", artistAlbums);
      return View(model);
    }

    [HttpPost("/artists/{artistId}/albums/")]
    public ActionResult Create(int artistId, string albumName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Artist foundArtist = Artist.Find(artistId);
      Album newAlbum = new Album(albumName);
      foundArtist.AddAlbum(newAlbum);
      List<Album> artistAlbums = foundArtist.Albums; 
      model.Add("albums", artistAlbums);
      model.Add("artist", foundArtist);
      return View("Show", model); 
    }

    [HttpGet("/artists/search")]
    public ActionResult Search()
    {
      return View(); 
    }

    [HttpGet("/artists/search/result")]
    public ActionResult Search()
    {
      return View(); 
    }
//     public async Task<IActionResult> Index(string searchString)
// {
//     var movies = from m in _context.Movie
//     select m;

//     if (!String.IsNullOrEmpty(searchString))
//     {
//         movies = movies.Where(s => s.Title.Contains(searchString));
//     }

//     return View(await movies.ToListAsync());
// }
  }
}