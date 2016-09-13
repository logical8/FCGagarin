using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FCGagarin.WebUI.Helpers
{
    public static class LinkPrepare
    {
        public static string RawTextToDB(string txt)
        {
            txt = txt.Replace("\r\n", "<br />");
            txt = Regex.Replace(txt, @"((http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-z‌​A-Z0-9]*)?/?([a-zA-Z‌​0-9\-\._\?\,\'/\\\+&‌​amp;%\$#\=~])*)", @"<a href='$1'>ссылка</a>");
            return txt;
        }
    }
}