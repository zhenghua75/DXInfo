using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FairiesCoolerCash.Business
{
    public class EkeyRef
    {
        [DllImport("DXInfo.Card.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool FairsVerify();

        [DllImport("DXInfo.Card.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool GetFairiesHardwareID([MarshalAs(UnmanagedType.LPStr)]StringBuilder strCardNo);

        [DllImport("DXInfo.Card.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool GetFairsKeyNo([MarshalAs(UnmanagedType.LPStr)]StringBuilder strCardNo);
    }
}
