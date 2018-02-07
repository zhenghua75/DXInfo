using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FairiesCoolerCash.Business
{
    public class CardRef
    {
        [DllImport("DXInfo.Card.dll")]
        public static extern Int16 CoolerReadCard([MarshalAs(UnmanagedType.LPStr)]StringBuilder strCardNo, ref Int32 value);

        [DllImport("DXInfo.Card.dll")]
        public static extern Int16 CoolerPutCard([MarshalAs(UnmanagedType.LPStr)]StringBuilder strCardNo);

        [DllImport("DXInfo.Card.dll")]
        public static extern Int16 CoolerRechargeCard([MarshalAs(UnmanagedType.LPStr)]StringBuilder strCardNo, Int32 value);

        [DllImport("DXInfo.Card.dll")]
        public static extern Int16 CancelCoolerRechargeCard([MarshalAs(UnmanagedType.LPStr)]StringBuilder strCardNo, Int32 value);

        [DllImport("DXInfo.Card.dll")]
        public static extern Int16 CoolerConsumeCard([MarshalAs(UnmanagedType.LPStr)]StringBuilder strCardNo, Int32 value);

        [DllImport("DXInfo.Card.dll")]
        public static extern Int16 CancelCoolerConsumeCard([MarshalAs(UnmanagedType.LPStr)]StringBuilder strCardNo, Int32 value);

        [DllImport("DXInfo.Card.dll")]
        public static extern Int16 CoolerRecycleCard();

            public static string GetStr(int st)
            {
                string str = "";
                switch (st)
                {                        
                    case 0:
                        str = "正确";
                        break;
                    case 1:
                        str = "无卡";
                        break;
                    case 2:
                        str = "CRC校验错";
                        break;
                    case 3:
                        str = "值溢出";
                        break;
                    case 4:
                        str = "非本系统卡";//"未验证密码";
                        break;
                    case 5:
                        str = "奇偶校验错";
                        break;
                    case 6:
                        str = "通讯出错";
                        break;
                    case 8:
                        str = "错误的序列号";
                        break;
                    case 10:
                        str = "验证密码失败";
                        break;
                    case 11:
                        str = "接收的数据位错误";
                        break;
                    case 12:
                        str = "接收的数据字节错误";
                        break;
                    case 14:
                        str = "Transfer错误";
                        break;
                    case 15:
                        str = "写失败";
                        break;
                    case 16:
                        str = "加值失败";
                        break;
                    case 17:
                        str = "减值失败";
                        break;
                    case 18:
                        str = "读失败";
                        break;
                    //case 97:
                    //    str = "未连接读卡器";
                    //    break;
                    case 98:
                        str = "初始化失败";
                        break;
                    case 99:
                        str = "卡号比对错误";
                        break;
                    default:
                        str = "未知错误："+st.ToString();
                        break;
                }
                return str;
            }
    }
}
