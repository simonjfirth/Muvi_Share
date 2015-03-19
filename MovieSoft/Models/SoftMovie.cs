using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieSoft.Helper.Movie;
using MovieSoft.DAL;

namespace MovieSoft.Models
{
    public class SoftMovie
    {
        public static readonly string BackgroundCoverLocation = "/Images/Movies/Background/";
        public static readonly string frontCoverLocation = "/Images/Movies/FrontCover/";

        MovieContext Context = new MovieContext();

        public int ID { get; set; }
        public int MovieDBID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string BackdropUrl { get; set; }
        public string ImageUrl { get; set; }
        public int VoteCount { get; set; }
        public double VoteAverage { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Overview { get; set; }

     

        public SoftMovie()
        {

        }

        public SoftMovie(int ID)
        {
            Context.SoftMovies.Find(ID);
        }

        public List<Genre> GetGenresForMovie()
        {
 

            List<Genre> list = (from gList in Context.MovieGenre
                        where gList.MovieID == this.MovieDBID
                        select gList.Genre
                            ).ToList();

            return list;
        }



        public SoftMovie(int mMovieDBID, string mTitle, string mFileName, int mVote, double mVoteAvg, DateTime? mRelease, string mOverview)
        {
            this.MovieDBID = mMovieDBID;
            this.Title = mTitle;
            this.FileName = mFileName;
            this.BackdropUrl = BackgroundCoverLocation + mMovieDBID.ToString() + ".jpg";
            this.ImageUrl = frontCoverLocation + mMovieDBID.ToString() + ".jpg";
            this.VoteCount = mVote;
            this.VoteAverage = mVoteAvg;
            this.ReleaseDate = mRelease;
            this.Overview = mOverview;
        }

        public void AddDetailsFromSearchMovie(TMDbLib.Objects.Search.SearchMovie m)
        {
            var movie = MovieHelper.GetMovie(m.Id);

            // check if it contains the genre, if not create it and add it
            // else create the link if it doesn't have it

            foreach (var movieGenre in movie.Genres)
            {
                Genre g = Context.Genre.Where(o => o.Name == movieGenre.Name).FirstOrDefault();

                // it doesn't exist so create it
                if (g == null)
                {
                    g = new Genre();
                    g.Name = movieGenre.Name;
                    Context.Genre.Add(g);
                    Context.SaveChanges();
                }

                // find the link, it doesn't exist add it
                var mvList = (from movies in Context.MovieGenre
                              where movies.MovieID == m.Id
                              && movies.Genre.ID == g.ID
                              select movies
                           ).ToList();
                if (mvList.Count <= 0)
                {
                    MovieGenre mG = new MovieGenre();
                    mG.Genre = g;
                    mG.MovieID = m.Id;
                    Context.MovieGenre.Add(mG);
                    Context.SaveChanges();
                }
            }



            // total genre list

            /*
            // init load only
            // add option to be able to refresh the entire list
            if (Context.Genre.Count() == 0)
            {
                foreach (var genre in s.Genres)
                {
                    Genre g = new Genre();
                    g.Name = genre.Name;
                    Context.Genre.Add(g);
                }
                Context.SaveChanges();
            }

            // map against its tmdbID
            if (Context.MovieGenre.Where(o => o.MovieID == m.Id).Count() == 0)
            {
                foreach (var gen in movie.Genres)
                {

                    MovieGenre mGenre = new MovieGenre();
                    // set ID
                    mGenre.MovieID = m.Id;

                    // find the ID of the genre
                   Genre g =  Context.Genre.Where(o => o.Name == gen.Name).FirstOrDefault();
                   mGenre.Genre = g;
                }

                Context.SaveChangesAsync();
            }
             * */
        }
    }
}