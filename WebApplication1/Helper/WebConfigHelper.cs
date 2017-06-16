using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1.Helper
{
    public class WebConfigHelper
    {
        public static string getAppSeeting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}