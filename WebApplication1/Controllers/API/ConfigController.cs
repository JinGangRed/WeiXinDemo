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
        public WXConfig getConfig(string url)
        {
            string nonceStr = WebConfigHelper.getAppSeeting(ConstantData.noncestr.ToString());
            string timestamp =  WebConfigHelper.getAppSeeting(ConstantData.timestamp.ToString());
            Dictionary<string, string> wxConfig = new Dictionary<string, string>();
            wxConfig.Add("noncestr", nonceStr);
            wxConfig.Add("timestamp",timestamp);
            wxConfig.Add("url", url);
            WeiXinHelper weiXinHelper = new WeiXinHelper();
            string signature = weiXinHelper.getSignature(wxConfig);
            //WeiXinHelper.getAccessToken();
            WXConfig config = new WXConfig
            {
                APPID = WebConfigHelper.getAppSeeting(ConstantData.cropid.ToString()),
                Timestamp = timestamp,
                NonceStr = nonceStr,
                Signature = signature
            };
            return config;
        }
    }
}
