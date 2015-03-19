using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.Requests.Parameters;
using Google.Apis.Download;
using Google.Apis.Http;
using System.Threading.Tasks;
using Google.Apis.Requests;


namespace MovieSoft.Helper.YouTube
{
    public class YouTubeHelper
    {
        protected readonly string API_KEY = "AIzaSyC63NOjL86CgW8C3S4sEX5VQACQLgNGm_g";

        /// <summary>
        /// Connect youtube service API
        /// </summary>
        /// <returns></returns>
        protected YouTubeService LoadService()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = API_KEY

            });
            return youtubeService;
        }


        /// <summary>
        /// Returns embeded video url for youtube from a movie trailer
        /// </summary>
        /// <param name="movieTitle">Movie trailer to play</param>
        /// <returns>Embded video urk</returns>
        public async Task<string> GetMovieHTML(string movieTitle)
        {
            var youtubeService = LoadService();
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = movieTitle + " trailer official"; // Replace with your search term.
            searchListRequest.MaxResults = 1;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            var searchListResponse = await searchListRequest.ExecuteAsync();
            if (searchListResponse.Items.Count > 0)
            {
      
                return "https://www.youtube.com/embed/" + searchListResponse.Items[0].Id.VideoId.ToString();
            }
            return "";
        }

    }
}