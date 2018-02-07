using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FairiesCoolerCash.Business
{
    public class OrderMenuRowBackgroudConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            int status = System.Convert.ToInt32(value);
            Brush brush = Brushes.White;
            switch (status)
            {
                case 0://正常
                    brush = Brushes.White;
                    break;
                case 1://退菜
                    brush = Brushes.Gray;
                    break;
                case 2://下单
                    brush = Brushes.White;
                    break;
                case 3://缺菜
                    brush = Brushes.White;
                    break;
                case 4://催菜
                    brush = Brushes.Red;
                    break;
                case 5://制作
                    brush = Brushes.Yellow;
                    break;
                case 6://出菜
                    brush = Brushes.GreenYellow;
                    break;
                case 7://出菜后退菜
                    brush = Brushes.White;
                    break;
                case 8://结账
                    brush = Brushes.White;
                    break;
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CupTypeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int getValue = (int)value;

            switch (getValue)
            {
                case 0:
                    return "大杯";
                //case 1:
                //    return "中杯";
                //case 2:
                //    return "小杯";
                default:
                    return "标准杯";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value as string;

            switch (s)
            {
                case "大杯":
                    return 0;
                //case "中杯":
                //    return 1;
                //case "小杯":
                //    return 2;
                default:
                    return -1;
            }
        }

        #endregion
    }
    public class OrderMenuStatusConverter : IValueConverter
    {
        private List<DXInfo.Models.MyEnum> lOrderMenuStatus;
        public OrderMenuStatusConverter()
        {
            lOrderMenuStatus = DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.OrderMenuStatus));
        }
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int getValue = (int)value;
            DXInfo.Models.MyEnum myEnum = lOrderMenuStatus.Find(f => f.Id == getValue);
            if (myEnum != null)
            {
                return myEnum.Name;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value as string;
            DXInfo.Models.MyEnum myEnum = lOrderMenuStatus.Find(f => f.Name == s);
            if (myEnum != null)
            {
                return myEnum.Id;
            }
            return 0;
        }

        #endregion
    }
    public class OrderMenuStatusEnabledConverter : IValueConverter
    {
        private List<DXInfo.Models.MyEnum> lOrderMenuStatus;
        public OrderMenuStatusEnabledConverter()
        {
            lOrderMenuStatus = DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.OrderMenuStatus));
        }
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int getValue = (int)value;
            if (getValue != (int)DXInfo.Models.OrderMenuStatus.Withdraw &&
                getValue != (int)DXInfo.Models.OrderMenuStatus.ReturnAfterOut)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //bool s = (bool)value as bool;
            //DXInfo.Models.MyEnum myEnum = lOrderMenuStatus.Find(f => f.Name == s);
            //if (myEnum != null)
            //{
            //    return myEnum.Id;
            //}
            //return 0;
            throw new NotImplementedException();
        }

        #endregion
    }
    public class DeskStatusConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int getValue = (int)value;

            switch (getValue)
            {
                case 0:
                    return @"..\..\images\desk-red.png";//开台
                case 3:
                    return @"..\..\images\desk-green.png";//下单
                case 1:
                    return @"..\..\images\desk-yellow.png";//预定
                case 2:
                    return @"..\..\images\desk-white.png";
                default:
                    return @"..\..\images\desk-white.png";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value as string;

            switch (s)
            {
                case "正常":
                    return 0;
                //case "中杯":
                //    return 1;
                //case "小杯":
                //    return 2;
                default:
                    return 1;
            }
        }

        #endregion
    }
    public class TotalSumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;
            var values = value as IEnumerable<object>;
            if (values == null)
                return 0;

            double sum = 0;

            foreach (dynamic u in values)
            {
                sum += u.Balance;
            }


            return sum.ToString("c");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }

    public class DecimalConverter : IValueConverter
    {
        //string
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;
            string retVal = value.ToString();
            if (retVal.Equals("-1")) return string.Empty;
            return value.ToString();
        }
        //decimal
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return -1;
            decimal retVal;
            return decimal.TryParse(value.ToString(), out retVal) ? retVal : -1;//: (decimal?)null;
            //return retValNull;
        }
    }
    public class DecimalNullConverter : IValueConverter
    {
        //string
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
        //decimal
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            if (value.ToString() == string.Empty) return null;
            //decimal retVal;
            //return decimal.TryParse(value.ToString(), out retVal) ? retVal : -1;//: (decimal?)null;
            //return retValNull;
            return value;
        }
    }
    public class ImageConverter : IValueConverter
    {
        private string ImageFilePath;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            this.ImageFilePath = value.ToString();
            string dir = ConfigurationManager.AppSettings["imageFilePath"];
            string path = Path.Combine(dir, value.ToString());
            if (File.Exists(path))
            {
                BitmapImage image = new BitmapImage(new Uri(path));
                return image;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return this.ImageFilePath;
        }
    }

    public class ReceiptStatusConverter : IValueConverter
    {
        private List<DXInfo.Models.MyEnum> lReceiptStatus;
        public ReceiptStatusConverter()
        {
            lReceiptStatus = DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.ReceiptStatus));
        }
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int getValue = (int)value;
            DXInfo.Models.MyEnum myEnum = lReceiptStatus.Find(f => f.Id == getValue);
            if (myEnum != null)
            {
                return myEnum.Name;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value as string;
            DXInfo.Models.MyEnum myEnum = lReceiptStatus.Find(f => f.Name == s);
            if (myEnum != null)
            {
                return myEnum.Id;
            }
            return 0;
        }

        #endregion
    }

    //public class MyCloneConverter : IValueConverter
    //{
    //    public static MyCloneConverter Instance = new MyCloneConverter();

    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is System.Windows.Freezable)
    //        {
    //            value = (value as System.Windows.Freezable).Clone();
    //        }

    //        return value;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotSupportedException();
    //    }

    //}
}
