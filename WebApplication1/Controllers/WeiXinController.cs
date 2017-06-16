using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    public class WeiXinController : Controller
    {
        // GET: WeiXin
        public ActionResult Index()
        {
            WeiXinHelper wexin = new WeiXinHelper();
            string tick = wexin.getJSAPI_Ticket();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            
            return View();
        }
    }
}