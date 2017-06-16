using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /**
     * 微信配置
     * 
     * 
     **/
    public class WXConfig
    {
        public string APPID { set; get; }
        public string Timestamp { set; get; }
        public string NonceStr { set; get; }
        public string Signature { set; get; }
    }
}