using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /// <summary>
    /// 存放从微信上得到的信息
    /// </summary>
    public class WXCookie
    {
        //Access_Token
        public string KeyValue { set; get; }
        //获得时间
        public DateTime Get_Key_Time { set; get; }
        //持续时间
        public Double Duration { set; get; }


    }
}