#region License

//Media glass - my simple WPF player
//Copyright (C) 2008-2010 Denis Yakimenko <denyakimenko@yandex.ru>

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Diagnostics;
using System.Threading;
using Media_glass.Common;

namespace Media_glass
{
    /// <summary>
    /// Useful url :
    /// http://www.iridescence.no/post/CreatingaSingleInstanceApplicationinC.aspx
    /// </summary>
    class EntryPoint
    {
        /// <summary>
        /// Application start point.
        /// </summary>
        /// <param name="args"></param>
        [System.STAThreadAttribute()]
        static void Main(string[] args)
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "Media Glass", out createdNew))
            {
                if (createdNew)
                {
                    App app = new App();

                    app.InitializeComponent();
                    app.Run();
                }
                else
                {
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            if (args.Length > 0)                                
                                SendMessage(process.MainWindowHandle.ToInt32(), args[0]);

                            Win32.SetForegroundWindow(process.MainWindowHandle);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Send message from one process to another.
        /// </summary>        
        static int SendMessage(int hWnd, string msg)
        {
            int result = 0;

            if (hWnd > 0)
            {
                byte[] sarr = System.Text.Encoding.Default.GetBytes(msg);
                int len = sarr.Length;
                Win32.COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = msg;
                cds.cbData = len + 1;
                result = Win32.SendMessage(hWnd, Win32.WM_COPYDATA, 0, ref cds);
            }

            return result;
        }       
    }

}
