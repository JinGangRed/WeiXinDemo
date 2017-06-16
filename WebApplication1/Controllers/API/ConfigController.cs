using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Controllers.API
{
    public class ConfigController : ApiController
    {
        public WXConfig getConfig()
        {
            WXConfig config = new WXConfig();

            config.APPID = WebConfigHelper.getAppSeeting("appId");
            config.NonceStr = WebConfigHelper.getAppSeeting("timestamp");
            config.Signature = WebConfigHelper.getAppSeeting("nonceStr");
            config.Timestamp = WebConfigHelper.getAppSeeting("signature");
            return config;
        }
    }
}
