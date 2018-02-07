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

namespace FormatRegistrClassLibrary
{
    /// <summary>
    /// Group of media format. 
    /// For exmple Wma group consist of ".wma" and ".wax";     
    /// </summary>
    public class MediaFormatGroup
    {
        /// <summary>
        /// Creates instance of MediaFormatGroup.
        /// </summary>
        /// <param name="formats">Format list assigned to this group : ".avi",".wmv",".wma" and etc.</formats>        
        public MediaFormatGroup(params string[] formats)
        {
            this.formats = formats;
        }

        /// <summary>
        /// Format list assigned to this group : ".avi",".wmv",".wma" and etc.
        /// </summary>
        string[] formats = null;

        /// <summary>
        /// Format list assigned to this group : ".avi",".wmv",".wma" and etc.
        /// </summary>
        public string[] Formats
        {
            get { return formats; }
            set { formats = value; }
        }        

        /// <summary>
        /// Is this format group assigned to our program or not.
        /// Return true if all format assigned, return false if one or more is not assigned.
        /// </summary>
        public bool IsAssigned
        {
            get
            {
                if (formats.Length == 0)
                    return false;

                foreach (string format in this.formats)                
                    if (!MediaFormatManager.IsAssign(format))
                        return false;                

                return true;
            }

            set
            {                
                foreach(string format in this.formats)
                    if (MediaFormatManager.IsAssign(format) != value)
                    {
                        if (value)
                            MediaFormatManager.Assign(format);
                        else
                            MediaFormatManager.UnAssign(format);
                    }
            }
        }
    }
}
