using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebUtility;

namespace AvaloniaApplication2
{
    class PlaylistDataProcessor
    {
        public PlaylistData Process(string url)
        {
            string patt = "https://www.nugs.net/live-download-";
            if (url.StartsWith(patt))
            {
                var htmlWeb = new HtmlWeb()
                {
                    OverrideEncoding = Encoding.GetEncoding("UTF-8")
                };
                var document = htmlWeb.Load(url);

                var songTitleNode = document?.DocumentNode?.SelectSingleNode("//div[@class=\"product-set-info\"]/div[2]/h3");
                var albumTitleNode = document?.DocumentNode?.SelectSingleNode("//div[@class=\"product-set-info\"]/div[2]/p");

                var songTitle = songTitleNode.InnerHtml.Trim();
                var albumTitle = albumTitleNode.InnerText.Trim();

                var songCollection = document?.DocumentNode?.SelectNodes("//span[@class=\"item-name\"]");

                var songsTitles = songCollection.Select(node => HtmlDecode(node.InnerText.Trim())).ToList();


                return new PlaylistData { ArtistName = songTitle, PlaylistTitle = albumTitle, Songs = songsTitles };
            }
            else
            {
                return new PlaylistData { ArtistName = "Invalid Url", PlaylistTitle = "Invalid Url"};
            }

        }
    }

    public class PlaylistData
    {
        public string ArtistName { get; set; }
        public string PlaylistTitle { get; set; }

        public List<string> Songs { get; set; }


    }
}