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
    /// Convert time to format wihtout zeroes .
    /// </summary>
    public static class ShortTime
    {
        /// <summary>
        /// Convert time to string. 
        /// h > 0 - h.mm.ss ( 5:06:09 )
        /// m > 0 - m.ss ( 34:08 )
        /// otherwise - s ( 4 )
        /// This method is used to show time span in a list view.
        /// </summary>        
        public static string TimeToString(TimeSpan timeSpan)
        {
            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            if (hours > 0)
                return String.Format("{0}:{1}:{2}", hours, minutes.ToString("00."), seconds.ToString("00."));

            if (minutes > 0)
                return String.Format("{0}:{1}", minutes, seconds.ToString("00."));

            return seconds.ToString();
        }
    }
}
