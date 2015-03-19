using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieSoft.DAL;

namespace MovieSoft.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Genre()
        { 
        
        }
    }

    public class MovieGenre
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public Genre Genre { get; set; }
   
        public MovieGenre()
        {
        
        }

        public MovieGenre(int mID)
        { 
            
        }

    }
}