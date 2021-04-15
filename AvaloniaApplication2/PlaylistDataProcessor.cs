using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebUtility;

namespace AvaloniaApplication2
{
    class PlaylistDataProcessor
    {
        protected const string WebcastPlaylistDataUrlPattern = "\\d-WEBCAST.html";

        protected const string PlaylistDataUrlPattern = "\\d+.html";

        public PlaylistDataResult Process(string urlToProcess)
        {
            if(!TryLoadDocument(urlToProcess, out HtmlDocument document))
            {
                return new PlaylistDataResult { IsSuccess = false, ErrorMessage = "The URL can't be processed" };
            }
            
            switch(urlToProcess)
            {
                case string url when Regex.IsMatch(url, WebcastPlaylistDataUrlPattern):
                    return GetWebcastPlaylistData(document);
                case string url when Regex.IsMatch(url, PlaylistDataUrlPattern):
                    return GetPlaylistData(document);                     
                default:
                    return new PlaylistDataResult {IsSuccess = false, ErrorMessage = "Invalid URL" };
            }
        }

        protected bool TryLoadDocument(string url, out HtmlDocument doc)
        {
            var website = new HtmlWeb
            {
                OverrideEncoding = Encoding.GetEncoding("utf-8")
            };
            doc = null;
            try
            {
                doc = website.Load(url);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static PlaylistDataResult GetWebcastPlaylistData(HtmlDocument document)
        {
            var playlists = new List<PlaylistWithSongs>();
            var playlistBox = document?.DocumentNode.SelectSingleNode("//div[@class=\"product-description hide\"]/p[3]")
                .ChildNodes.Where(x => x.Name == "#text")
                .Skip(1).Select(node => HtmlDecode(node.InnerText.Trim()))
                .ToList();

            var playlistTitle = document?.DocumentNode.SelectSingleNode("//div[@class=\"product-description hide\"]/p[3]")
                .ChildNodes.Where(x => x.Name == "#text").FirstOrDefault().InnerText;

            var playlist = new PlaylistWithSongs { PlaylistTitle = playlistTitle, Songs = playlistBox };

            playlists.Add(playlist);
            var artistName = HtmlDecode(document?.DocumentNode.SelectSingleNode("//h3[@class=\"ppv-artistname\"]").InnerText);

            return new PlaylistDataResult { IsSuccess = true, ArtistName = artistName, Playlists = playlists };
        }

        private static PlaylistDataResult GetPlaylistData(HtmlDocument document)
        {
            var playlists = new List<PlaylistWithSongs>();
            var songTitleNode = document?.DocumentNode?.SelectSingleNode("//div[@class=\"product-set-info\"]/div[2]/h3");
            var albumTitleNode = document?.DocumentNode?.SelectSingleNode("//div[@class=\"product-set-info\"]/div[2]/p");

            var songTitle = HtmlDecode(songTitleNode.InnerHtml.Trim());
            var albumTitle = albumTitleNode.InnerText.Trim();

            var songCollection = document?.DocumentNode?.SelectNodes("//span[@class=\"item-name\"]");

            var songsTitles = songCollection.Select(node => HtmlDecode(node.InnerText.Trim())).ToList();

            var playlistTitles = document?.DocumentNode?.SelectNodes("//div[@class = \"product-set-container\"]");

            foreach (var node in playlistTitles)
            {
                PlaylistWithSongs plays = new PlaylistWithSongs
                {
                    PlaylistTitle = node.ChildNodes.FirstOrDefault(x => x.Name == "div")
                    .ChildNodes.FirstOrDefault(x => x.Name == "h4")
                    .InnerText,
                    Songs = node.SelectNodes(".//span[@class = \"item-name\"]").Select(x => HtmlDecode(x.InnerText).Trim()).ToList()
                };
                playlists.Add(plays);
            }

            return new PlaylistDataResult { IsSuccess = true, ArtistName = songTitle, Playlists = playlists };
        }
    }

    public class PlaylistDataResult
    {
        public string ArtistName { get; set; }

        public List<PlaylistWithSongs> Playlists { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class PlaylistWithSongs
    {
        public string PlaylistTitle { get; set; }

        public List<string> Songs { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(PlaylistTitle);
            sb.AppendLine();
            sb.AppendLine();

            foreach (var song in Songs)
            {
                sb.Append(song);
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}