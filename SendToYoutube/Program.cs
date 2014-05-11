using System;
using System.Text.RegularExpressions;
using System.Configuration;

namespace SendToYoutube
{
    class MainClass
    {
        static void Usage()
        {
            Console.WriteLine("Usage: SendToYoutube <mediacenter api link> <youtube link>");
        }

        public static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Usage();
                return 1;
            }

            var mcUriConfig = ConfigurationManager.AppSettings["mc:" + args[0]];
            var mcUriBuilder = new UriBuilder(string.IsNullOrEmpty(mcUriConfig) ? args[0] : mcUriConfig);
            mcUriBuilder.Path = "/jsonrpc";
            var mcUri = mcUriBuilder.Uri;

            string videoId = "";

            try
            {
                var youtubeQueryString = args[1].Substring(args[1].IndexOf('?'));
                var youtubeParameters = System.Web.HttpUtility.ParseQueryString(youtubeQueryString);
                videoId = youtubeParameters["v"];

                var re = new Regex(@"^[\d\w-_]+$");

                if (!re.IsMatch(videoId))
                {
                    throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Error parsing youtube uri");
                return 2;
            }

            try
            {
                var jsonRpc = @"{'jsonrpc': '2.0', 'method': 'Player.Open', 'params':{ 'item' : {'file' : 'plugin://plugin.video.youtube/?action=play_video&videoid="
                              + videoId + "' }}, 'id': 1}";
                jsonRpc = jsonRpc.Replace("'", "\"");

                var wc = new System.Net.WebClient();
                wc.Headers["Content-Type"] = "application/json";
                var s = wc.UploadString(mcUri, "POST", jsonRpc);

                Console.WriteLine(s);
            }
            catch
            {
                Console.WriteLine("posting a link to xbmc");
                return 3;
            }

            return 0;
        }
    }
}
