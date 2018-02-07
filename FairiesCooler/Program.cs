using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace FairiesCooler
{
    static class Program
    {
        private static Assembly assembly = Assembly.GetExecutingAssembly();
        private static string assemblyPath = Program.assembly.GetName().Name.Replace(' ', '_');



        public static System.IO.Stream GetResource(string fileName)
        {
            return Program.assembly.GetManifestResourceStream(Program.assemblyPath + '.' + fileName);
        }
        public static Image SplashScreen = Image.FromStream(Program.GetResource("dxlogo.png"));
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //Application.Run(new AboutBox1());
        }
    }
}
