using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1.Helper
{
    /**
     * 用于获得Web配置信息的工具类
     */
    public class WebConfigHelper
    {
        public static string getAppSeeting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}