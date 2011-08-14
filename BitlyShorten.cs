using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace WP7NetHelpers
{
    public class BitlyShorten
    {
        private const string BITLY_LOGIN = "YOUR_LOGIN_HERE";
        private const string BITLY_API_KEY = "YOUR_API_HERE";
        private Action<string> _callback = null;

        public void Shorten(string longUrl, Action<string> callback)
        {
            _callback = callback;

            string url = string.Format(@"http://api.bit.ly/v3/shorten?login={0}&apiKey={1}&longUrl={2}&format=txt",
                         BITLY_LOGIN, BITLY_API_KEY, HttpUtility.UrlEncode(longUrl));

            WebClient wc = new WebClient();
            wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
            wc.OpenReadAsync(new Uri(url));
        }

        void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Stream stream = e.Result;
            var reader = new StreamReader(stream); 
            var shortenedUrl = reader.ReadToEnd();
            _callback(shortenedUrl);
        }
    }
}
