using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /// <summary>
    /// 微信JSSDK的返回
    /// </summary>
    public class WXJSSDK
    {
        public WXJSSDK()
        {
        }
        string _errcode;

        public string errcode
        {
            get { return _errcode; }
            set { _errcode = value; }
        }
        string _errmsg;

        public string errmsg
        {
            get { return _errmsg; }
            set { _errmsg = value; }
        }
        string _ticket;

        public string ticket
        {
            get { return _ticket; }
            set { _ticket = value; }
        }
        string _expires_in;

        public string expires_in
        {
            get { return _expires_in; }
            set { _expires_in = value; }
        }





    }
}