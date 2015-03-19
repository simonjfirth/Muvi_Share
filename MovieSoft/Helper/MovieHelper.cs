using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMDbLib.Client;

namespace MovieSoft.Helper.Movie
{
    public class MovieHelper
    {
        protected static TMDbClient client = new TMDbClient("ad8f4929641b7243b42b454cca9916d7");
      

        /// <summary>
        /// Gets Movie Object from The Movie DB ID
        /// </summary>
        /// <param name="mMovieTMDBID"></param>
        /// <returns></returns>
        public static TMDbLib.Objects.Movies.Movie GetMovie(int mMovieTMDBID)
        {
            var movie = client.GetMovie(mMovieTMDBID);
            return movie;
        }
    }
}