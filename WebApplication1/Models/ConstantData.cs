using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /*
     * 一些常量数据
     */ 
    public enum ConstantData
    {
        //微信相关
        [Description("CropId")]
        cropid, 
        [Description("CorpSecret")]
        corpsecret,
        [Description("Timestamp")]
        timestamp,
        [Description("NonceStr")]
        noncestr

       
    }
}