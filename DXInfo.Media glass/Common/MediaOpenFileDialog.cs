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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FormatRegistrClassLibrary;

namespace Media_glass.Common
{
    /// <summary>
    /// My custom dialog for opening media files.
    /// </summary>
    class MediaOpenFileDialog
    {
        /// <summary>
        /// Convert string format list to format line.
        /// </summary>        
        static string GetExtensionString(List<string> formats)
        {
            StringBuilder builder = new StringBuilder();

            foreach (string format in formats)
            {
                builder.Append('*');
                builder.Append(format);
                builder.Append(';');
            }

            if(builder.Length>0)
                builder.Remove(builder.Length-1, 1);

            return builder.ToString();
        }

        /// <summary>
        /// Create new OpenFileDialog with the specific settings.
        /// </summary>
        public static OpenFileDialog New
        {            
            get
            {
                List<string> videoExtensions = MediaFormatGroups.AllVideoExtensions;
                List<string> audioExtensions = MediaFormatGroups.AllAudioExtensions;

                string audioExtensionLine = GetExtensionString(audioExtensions);
                string videoExtensionLine = GetExtensionString(videoExtensions);

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;                                
                dlg.Filter = String.Format("Video and audio files|{0};{1}|Video files|{0}|Audio files|{1}|All files|*.*", videoExtensionLine, audioExtensionLine);
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.Multiselect = true;
                return dlg;
            }
        }
    }
}
