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

namespace Media_glass.Common.Helpers
{
    /// <summary>
    /// http://www.vcskicks.com/csharp_filesize.php
    /// http://dotnetperls.com/convert-bytes-megabytes
    /// Get formatted file size info.
    /// </summary>
    public static class FileSize
    {
        /// <summary>
        /// Get file size info.
        /// </summary>        
        public static string GetValue(double byteCount)
        {
            string size = BytesToSize(byteCount);

            if (byteCount > 0)
                size = String.Format("{0} ({1})", size, GetByteCount(byteCount));

            return size;
        }

        /// <summary>
        /// Convert bytes to the nearest value ( KB, MB, GB )
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        private static string BytesToSize(double byteCount)
        {
            string size = "0 bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " bytes";

            return size;
        }

        /// <summary>
        /// Convert bytes into smart format.
        /// </summary>        
        private static string GetByteCount(double byteCount)
        {
            return String.Format
                (
                "{0} bytes",
                String.Format("{0:### ### ### ### ### ##0}", byteCount).TrimStart()
                );
        }
    }
}
