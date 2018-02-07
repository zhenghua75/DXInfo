using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FairiesCoolerCash.Business;

namespace FairiesCoolerCash
{
    /// <summary>
    /// SplashScreenWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        public SplashScreenWindow(string splashScreenImgPath)
        {
            InitializeComponent();

            if (Helper.ResourceExists("images/"+splashScreenImgPath))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/" + "images/"+splashScreenImgPath, UriKind.Absolute);
                bitmap.EndInit();
                splashScreenFile.Height = bitmap.PixelHeight;
                splashScreenFile.Width = bitmap.PixelWidth;
                splashScreenFile.Source = bitmap;
            }
            string strpath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filepath = System.IO.Path.Combine(strpath, splashScreenImgPath);
            if (System.IO.File.Exists(filepath))
            { 
                BitmapImage splashScreenImage = new BitmapImage(new Uri(filepath));
                splashScreenFile.Height = splashScreenImage.PixelHeight;
                splashScreenFile.Width = splashScreenImage.PixelWidth;
                splashScreenFile.Source = splashScreenImage;
            }
        }
    }
}
