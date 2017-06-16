using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Helper
{
    
    /*
     * 微信相关功能的处理类
     */
    public class WeiXinHelper
    {
        //请求token的URL地址
        private static string url_token = string.Format(
            "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}",
            WebConfigHelper.getAppSeeting(ConstantData.cropid.ToString()),
            WebConfigHelper.getAppSeeting(ConstantData.corpsecret.ToString()));
        private static string url_jsapi = string.Format(
            "https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}",
            getAccessToken());

        //Cookie
        private static HttpCookie cookie = null;
        /// <summary>
        /// 获得Access_Token
        /// </summary>
        /// <returns></returns>
        private static string getAccessToken()
        {
            LogHelper.Info("获取Access_Token:");
            //从cookie中获得access_token
            cookie = HttpContext.Current.Request.Cookies["access_token"];
            if (cookie != null)
            {
                LogHelper.Info("\t存在于Cookie");
                var cookie_entity = JsonHelper.ParseFromJson<WXCookie>(cookie.Value);
                //如果Access_Token没过期
                if (string.IsNullOrEmpty(cookie_entity.KeyValue) && DateTime.Now <= cookie_entity.Get_Key_Time.AddSeconds(cookie_entity.Duration))
                {
                    LogHelper.Info("\t\tCookie中的Access_Token尚未过期");
                    LogHelper.Info("\t\t在cookie中的获得的Access_Token是：" + cookie_entity.KeyValue);
                    return cookie_entity.KeyValue;
                }
                else
                {
                    LogHelper.Info("\t\tCookie中的Access_Token已过期,重新获取");
                    //重新获取Access_Token 
                    return getAccessTokenFromWeChat();
                }
            }
            else
            {
                LogHelper.Info("\tCookie不存在，开始获取");
                //获取Access_Token 
                return getAccessTokenFromWeChat();
            }
        }
        /// <summary>
        /// 从微信端获得Accesstoken
        /// </summary>
        /// <returns></returns>
        private static string getAccessTokenFromWeChat()
        {
            WXToken token = JsonHelper.ParseFromJson<WXToken>(HttpHelper.requestUrl(url_token));
            double duration = 7200;
            Double.TryParse(token.expires_in,out duration);
            cookie = new HttpCookie("access_token");
            WXCookie cookieEntity = new WXCookie
            {
                KeyValue = token.access_token,
                Duration = duration,
                Get_Key_Time = DateTime.Now
            };
            cookie.Value = JsonHelper.GetJson<WXCookie>(cookieEntity);
            HttpContext.Current.Request.Cookies.Add(cookie);
            LogHelper.Info("\t从微信端获得的Access_Token是：" + token.access_token);
            return token.access_token;
        }
        /// <summary>
        /// 获得JSSDK的ticket
        /// </summary>
        /// <returns></returns>
        public string getJSAPI_Ticket(){
            LogHelper.Info("获取JSAPI_Ticket:");
            cookie = HttpContext.Current.Request.Cookies["JSAPI_Ticket"];
            if (cookie != null)
            {
                LogHelper.Info("\t存在于Cookie");
                var cookie_entity = JsonHelper.ParseFromJson<WXCookie>(cookie.Value);
                //如果Access_Token没过期
                if (string.IsNullOrEmpty(cookie_entity.KeyValue) && DateTime.Now <= cookie_entity.Get_Key_Time.AddSeconds(cookie_entity.Duration))
                {
                    LogHelper.Info("\t\tCookie中的JSAPI_Ticket尚未过期");
                    LogHelper.Info("\t\t在cookie中的获得的JSAPI_Ticket是：" + cookie_entity.KeyValue);
                    return cookie_entity.KeyValue;
                }
                else
                {
                    LogHelper.Info("\t\tCookie中的JSAPI_Ticket已过期,重新获取");
                    //重新获取Access_Token 
                    return getJSAPI_TicketFromWeChat();
                }
            }
            else
            {
                LogHelper.Info("\tCookie不存在，开始获取");
                //获取Access_Token 
                return getJSAPI_TicketFromWeChat();
            }
        }
        /// <summary>
        /// 从微信端获取jsapi_ticket
        /// </summary>
        /// <returns></returns>
        private static string getJSAPI_TicketFromWeChat(){
            WXJSSDK wxjssdk = JsonHelper.ParseFromJson<WXJSSDK>(HttpHelper.requestUrl(url_jsapi));
            double duration = 7200;
            Double.TryParse(wxjssdk.expires_in, out duration);
            cookie = new HttpCookie("JSAPI_Ticket");
            WXCookie cookieEntity = new WXCookie
            {
                KeyValue = wxjssdk.ticket,
                Duration = duration,
                Get_Key_Time = DateTime.Now
            };
            cookie.Value = JsonHelper.GetJson<WXCookie>(cookieEntity);
            HttpContext.Current.Request.Cookies.Add(cookie);
            LogHelper.Info("\t从微信端获得的Access_Token是：" + wxjssdk.ticket);
            return wxjssdk.ticket;
        }
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="wxConfig"></param>
        /// <returns></returns>
        public string getSignature(Dictionary<string,string> wxConfig)
        {
            wxConfig.Add("jsapi_ticket",getJSAPI_Ticket());
            string str = getStr(wxConfig);
            SHA1 sha1 = SHA1.Create();
            UTF8Encoding enc = new UTF8Encoding();
            byte[] bytes = enc.GetBytes(str);
            byte[] dataHashed = sha1.ComputeHash(bytes);
            string hash = BitConverter.ToString(dataHashed).ToLower().Replace("-", "");
            return hash;
        }
        /// <summary>
        /// 字典排序后拼接成字符串
        /// </summary>
        /// <param name="wxConfig"></param>
        /// <returns></returns>
        private string getStr(Dictionary<string, string> wxConfig)
        {
            var dicSort = from obj in wxConfig
                          orderby obj.Key
                          select obj;

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> key in dicSort)
            {
                sb.Append(key.Key + "=" + key.Value+"&");
            }
            return sb.ToString().Remove(sb.ToString().Length-1);
        }
    }
}