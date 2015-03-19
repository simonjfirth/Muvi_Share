using MovieSoft.DAL;
using MovieSoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDbLib.Client;
using MovieSoft.Helper.YouTube;
using MovieSoft.Helper.Movie;
using System.Threading.Tasks;




namespace MovieSoft.Controllers
{
    public class MovieController : Controller
    {
        MovieContext context = new MovieContext();

        // GET: Movie
        // GET: Search
        public ActionResult Index()
        {
           
            if (context.SoftMovies.Count() > 0)
            {
                return View(context.SoftMovies.ToList());
            }
            return View();
        }



        public async Task<ActionResult> Movie(int? ID)
        {
            if (ID == null)
            {
                return RedirectToAction("Index");
            }

            MovieContext context = new MovieContext();
            SoftMovie foundMovie = context.SoftMovies.Find(ID);
            ViewBag.BackdropUrl = foundMovie.BackdropUrl;

            var tmdbMovie = MovieHelper.GetMovie(foundMovie.MovieDBID);

            // view bag stuff, add all the database later on
            ViewBag.RunTime = tmdbMovie.Runtime;
            ViewBag.language = tmdbMovie.SpokenLanguages.Select(p => p.Name).ToList();
            ViewBag.ReleaseDate = (tmdbMovie.ReleaseDate.HasValue) ? tmdbMovie.ReleaseDate.Value.Date.ToShortDateString() : "";

            // get the youtube video link
            YouTubeHelper u = new YouTubeHelper();
            ViewBag.VideoUrl = await u.GetMovieHTML(tmdbMovie.Title);

  
            if (tmdbMovie.SimilarMovies != null)
            {
                List<int> similarMovieID = tmdbMovie.SimilarMovies.Results.Select(o => o.Id).ToList();
                List<SoftMovie> foundMovies = context.SoftMovies.Where(p => similarMovieID.Contains(p.MovieDBID)).ToList();
                ViewBag.SimilarMovies = foundMovies;
            }
       
            return View(foundMovie);
        }



        [HttpGet]
        public  ActionResult GetRelatedFilms(int ID)
        {
            var model = GetGenres(ID);
            return PartialView("GetRelatedFilms", model);
        }



        private  List<SoftMovie> GetGenres(int ID = 0)  
        {
            var movies = (from m in context.MovieGenre
                          where m.Genre.ID == ID
                          select m.MovieID
                           ).ToList();
            var movirList = context.SoftMovies.Where(o => movies.Contains(o.MovieDBID)).ToList();

            return movirList;
        }

    }
}