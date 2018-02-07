using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    public class MyReg
    {
        public const string FixedTelephone = @"\d{7}|\d{8}|\d{3}-\d{8}|\d{4}-\d{7}|\d{4}-\d{8}";
        public const string MobilePhone = @"^(13[0-9]|15[0|3|6|7|8|9]|18[8|9])\d{8}$";
        //匹配格式：11位手机号码3-4位区号，7-8位直播号码，1－4位分机号 如：12345678901、1234-12345678-1234
        public const string Phone = @"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)";
        /// <summary>
        /// 正浮点数
        /// </summary>
        public const string PlusNumber = @"^\d+(\.\d+)?$";
        /// <summary>
        /// 图片文件名
        /// </summary>
        public const string ImageFileName = @"[.](jpg|gif|bmp|png)$";
    }
}
