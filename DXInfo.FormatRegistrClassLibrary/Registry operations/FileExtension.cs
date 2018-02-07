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
using Microsoft.Win32;

namespace FormatRegistrClassLibrary
{    
    /// <summary>
    /// File format extension stored in registry.
    /// </summary>
    class FileExtension
    {
        /// <summary>
        /// Creates instance of FileExtension.
        /// </summary>
        /// <param name="extension">Format extension for example : ".avi"</param>
        public FileExtension(string extension)
        {
            this.extension = extension;
        }

        /// <summary>
        /// Extension, for example : ".avi"
        /// </summary>
        string extension;

        /// <summary>
        /// Create new file extension. If it already exists it will be cleared.
        /// </summary>
        /// <param name="programName">program name assigned to this format</param>
        public void Create(string programName)
        {
            if (this.Exists)
                this.Delete();

            Registry.ClassesRoot.CreateSubKey(this.extension).Close();
            this.ProgramName = programName;            
        }

        /// <summary>
        /// Is this format already exists in registry
        /// </summary>
        public bool Exists
        {
            get
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(extension))                
                    return key != null;                
            }
        }

        /// <summary>
        /// Program name assigned to this format.
        /// </summary>
        public string ProgramName
        {
            get
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(this.extension))
                {
                    if (key != null)
                        return Convert.ToString(key.GetValue(String.Empty));
                    else
                        return null;
                }
            }

            set
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(this.extension,true))
                {
                    if (key != null)
                        key.SetValue(null, value);                    
                }               
            }
        }        

        /// <summary>
        /// Remove this extension from registry.
        /// </summary>
        public void Delete()
        {
            if (!this.Exists)
                return;

            Registry.ClassesRoot.DeleteSubKeyTree(this.extension);
        }
    }
}
