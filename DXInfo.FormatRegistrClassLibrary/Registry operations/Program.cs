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
    /// Program info stored at the registry.
    /// </summary>
    class Program
    {   
        /// <summary>
        /// Creates instance of Program.
        /// </summary>
        /// <param name="programName"></param>
        public Program(string programName)
        {
            this.programName = programName;
        }

        /// <summary>
        /// Current program name
        /// </summary>
        string programName;

        /// <summary>
        /// Add program info to the registry. 
        /// If it already exists it will be removed and added again.
        /// </summary>
        /// <param name="exePath">Path to the exe program</param>
        public void Create(string exePath)
        {
            if (this.Exists)
                Delete();

            using (RegistryKey root = Registry.ClassesRoot.CreateSubKey(this.programName))
            {
                if (root != null)
                {
                    RegistryKey shellKey = root.OpenSubKey(RegistrySubKeys.Shell, true);

                    if (shellKey == null)
                        shellKey = root.CreateSubKey(RegistrySubKeys.Shell);

                    RegistryKey openKey = shellKey.OpenSubKey(RegistrySubKeys.Open, true);

                    if (openKey == null)
                        openKey = shellKey.CreateSubKey(RegistrySubKeys.Open);

                    RegistryKey commandKey = openKey.OpenSubKey(RegistrySubKeys.Command, true);

                    if (commandKey == null)
                        commandKey = openKey.CreateSubKey(RegistrySubKeys.Command);

                    commandKey.SetValue(string.Empty, String.Concat(exePath, " %1"), RegistryValueKind.ExpandString);

                    commandKey.Close();
                    openKey.Close();
                    shellKey.Close();

                    DefaultIcon = exePath;
                }
            }
        }

        /// <summary>
        /// Is program info already exists at the registry.
        /// </summary>
        public bool Exists
        {
            get
            {
                if (this.programName == String.Empty)
                    return false;

                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(this.programName))
                    return key != null;                
            }
        }

        /// <summary>
        /// Default icon assign to program and format opened by it.
        /// </summary>
        public string DefaultIcon
        {
            set
            {
                using (RegistryKey rootKey = Registry.ClassesRoot.OpenSubKey(this.programName,true))
                {
                    if (rootKey != null)
                    {
                        RegistryKey programKey = rootKey.OpenSubKey(RegistrySubKeys.DefaultIcon, true);
                        
                        if (programKey == null)                        
                            programKey = rootKey.CreateSubKey(RegistrySubKeys.DefaultIcon);

                        programKey.SetValue(string.Empty, value, RegistryValueKind.ExpandString);

                        programKey.Close();
                    }
                }
            }
        }
        
        /// <summary>
        /// Remove program info from the registry.
        /// </summary>        
        public void Delete()
        {
            if (!this.Exists)
                return;

            Registry.ClassesRoot.DeleteSubKeyTree(this.programName);
        }
    }
}
