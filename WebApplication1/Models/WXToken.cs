using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class WXToken
    {
        public WXToken()
        {

        }
        string _access_token;
        string _expires_in;
        public string access_token
        {
            get { return _access_token; }
            set { _access_token = value; }
        }
        public string expires_in
        {
            get { return _expires_in; }
            set { _expires_in = value; }
        }
             
    }
}