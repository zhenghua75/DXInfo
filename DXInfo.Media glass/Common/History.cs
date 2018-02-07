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
using Media_glass.Properties;

namespace Media_glass.Common
{
    /// <summary>
    /// Recentrly played files history.
    /// </summary>
    public static class History
    {
        #region public methods

        /// <summary>
        /// Get history list. Max size is 5.
        /// </summary>        
        public static List<string> GetRecentPlayedList()
        {
            return history;
        }

        /// <summary>
        /// Add file to history.
        /// </summary>        
        public static void Add(string file)
        {
            history.Insert(0, file);

            while (history.Count > 5)
                history.RemoveAt(history.Count - 1);

            Write();
        }

        /// <summary>
        /// Clear all lists.
        /// </summary>
        public static void Clear()
        {
            history.Clear();
            Write();
        }        

        #endregion

        #region private fields, properties, methods

        /// <summary>
        /// History collection.
        /// </summary>
        static List<string> history = Read();

        /// <summary>
        /// All history stored in one line separeted by ;
        /// Example : "File1.wmv;File2.avi"
        /// </summary>
        const char separator = ';';        

        /// <summary>
        /// Load history from setting line to list.
        /// </summary>        
        static List<string> Read()
        {
            if (String.IsNullOrEmpty(LineHistory))
                return new List<string>();

            return new List<string>(LineHistory.Split(separator));
        }        

        /// <summary>
        /// Write history list to setting line.
        /// </summary>
        static void Write()
        {
            LineHistory = String.Join(separator.ToString(), history.ToArray());
        }                       

        /// <summary>
        /// History from setttings.
        /// </summary>
        static string LineHistory
        {
            get
            {
                return Settings.Default.History;
            }

            set
            {
                Settings.Default.History = value;
            }
        }

        #endregion
    }
}
