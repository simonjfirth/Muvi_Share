using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDbLib.Client;
using TMDbLib.Objects.Search;
using TMDbLib.Utilities;
using System.IO;
using TMDbLib.Objects.General;
using MovieSoft.Models;
using System.Net;
using System.ComponentModel;
using MovieSoft.DAL;

namespace MovieSoft.Controllers
{
    public class SearchController : Controller
    {
        MovieContext context = new MovieContext();

        // GET: Search
        public ActionResult Index()
        {
            if(context.SoftMovies.Count() > 0)
            {
                return View(context.SoftMovies.OrderBy(o=> o.Title).ToList());
            }
            return View();
        }


        public ActionResult Search()
        {
            // get the local movies
            List<string> movies = getLocalMovieCollection();
            List<string> currentMovies = context.SoftMovies.Select(o => o.FileName).ToList();
            List<string> newItemsFound = movies.Except(currentMovies).ToList();
            return View(newItemsFound.OrderBy(o => o).ToList());
        }



        public ActionResult Save()
        {

            MovieContext context = new MovieContext();
            List<SoftMovie> movies = createIMDBColletion(getLocalMovieCollection());

            // clear out all of the movies
            List<SoftMovie> oldMovies = context.SoftMovies.ToList();
            oldMovies.ForEach(p => context.SoftMovies.Remove(p));
            movies.ForEach(p=> context.SoftMovies.Add(p));
            context.SaveChanges();

            return View("Index", movies.OrderBy(p=> p.Title).ToList());

        }


        protected List<string> getLocalMovieCollection()
        {
            List<string> mCollection = new List<string>();

            // get all the movies
            // format the movies into a readable "NORMAL" format
            mCollection = Directory.GetFiles(@"d:\Movielist\").Select(Path.GetFileNameWithoutExtension)
                                                            .Select(p => p.ToString().Replace('_', ' ').Replace('.', ' '))
                                                            .ToList();

            return mCollection;
        }

        /// <summary>
        /// Gets a list of Search Movie objects which have been found 
        /// </summary>
        /// <param name="mMovieList">List of movies to search for</param>
        /// <returns></returns>
        protected List<SoftMovie> createIMDBColletion(List<string> mMovieList)
        {
            WebClient client = new WebClient();
            List<SoftMovie> foundMoviesList = new List<SoftMovie>();
            TMDbClient movieClient = new TMDbClient("7b5e30851a9285340e78c201c4e4ab99");

            foreach (var movie in mMovieList)
            {
                SearchContainer<SearchMovie> m = movieClient.SearchMovie(movie);
                if (m.Results.Count > 0)
                {
                    SearchMovie foundMovie = m.Results[0];
                    var moreInfo= movieClient.GetMovie(foundMovie.Id, TMDbLib.Objects.Movies.MovieMethods.Keywords);

                    string overview = moreInfo.Overview;

                    SoftMovie s = new SoftMovie(foundMovie.Id, foundMovie.Title, movie, foundMovie.VoteCount, foundMovie.VoteAverage, foundMovie.ReleaseDate, overview);                   
                    s.AddDetailsFromSearchMovie(foundMovie);

                    foundMoviesList.Add(s);

                    string frontCoverUrl = foundMovie.PosterPath;
                    string backdrop = foundMovie.BackdropPath;

                    string backgroundServerLocation = Server.MapPath(SoftMovie.BackgroundCoverLocation) + s.MovieDBID.ToString() + ".jpg";
                    string frontCoverServerLocation = Server.MapPath(SoftMovie.frontCoverLocation) + s.MovieDBID.ToString() + ".jpg";
                    string backgroundWebUrl = "http://image.tmdb.org/t/p/w1000/" + foundMovie.BackdropPath;
                    string frontCoverWebUrl = "http://image.tmdb.org/t/p/w300/" + foundMovie.PosterPath;

                    downloadCoverToLocal(client, backgroundWebUrl, backgroundServerLocation);
                    downloadCoverToLocal(client, frontCoverWebUrl, frontCoverServerLocation);
                }
            }
            return foundMoviesList;
        }




        protected void downloadCoverToLocal(WebClient mClient, string mDownloadUrl, string mSaveLocationUrl)
        {
            if (!System.IO.File.Exists(mSaveLocationUrl))
            {
                mClient.DownloadFile(new Uri(mDownloadUrl), @mSaveLocationUrl);
            }
        }
    } 
}