using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApplication1.Helper
{
    public class HttpHelper
    {
        /*
         * 请求URL
         */ 
        public static string requestUrl(string url,string request_method = "GET"){
            if (string.IsNullOrEmpty(url))
            {
                return "URL不能为空";
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = request_method.ToUpper();
            request.ContentType = "application/x-www-form-urlencoded";
            var result = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(result.GetResponseStream());
            string content = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
            LogHelper.Info("请求的结果是:" + content);
            return content;
        }
    }
}