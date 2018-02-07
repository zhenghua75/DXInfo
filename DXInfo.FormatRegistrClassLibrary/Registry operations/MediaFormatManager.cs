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
    /// - Add extension and program info at regitstry;
    /// - Assign program to extension;
    /// - Unassign program to extension;
    /// - Remove our association;
    /// </summary>
    static class MediaFormatManager
    {
        /// <summary>
        /// Our program name
        /// </summary>
        const string programName = "Media Glass";        
        
        /// <summary>
        /// Add program info to the registry;
        /// </summary>
        /// <param name="executablePath">path to the program exe</param>
        public static void CreateProgram(string executablePath)
        {
            Program program = new Program(MediaFormatManager.programName);

            if (!program.Exists)
                program.Create(executablePath);
            else
                program.DefaultIcon = executablePath;

            SystemNotification.RegistrSettingsIsChanged();            
        }

        /// <summary>
        /// Remove program from the registry. It doesn't clear extension reference to the program.
        /// It only remove program data.
        /// </summary>
        public static void ClearAllAssociation()
        {
            Program program = new Program(MediaFormatManager.programName);
            if (program.Exists)
                program.Delete();

            SystemNotification.RegistrSettingsIsChanged();
        }

        /// <summary>
        /// Get program associated to this format extension.
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetProgramAssociateToFormat(string extension)
        {
            FileExtension fe = new FileExtension(extension);

            if (fe.Exists)
                return fe.ProgramName;
            else
                return null;
        }

        /// <summary>
        /// Is this file extension assigned to our program.
        /// </summary>
        /// <param name="extension">For example ".avi"</param>        
        public static bool IsAssign(string extension)
        {
            return GetProgramAssociateToFormat(extension) == programName;           
        }

        /// <summary>
        /// Assign our program to file extension.
        /// </summary>
        /// <param name="extension">For example ".avi"</param>
        public static void Assign(string extension)
        {
            FileExtension fe = new FileExtension(extension);

            if (!fe.Exists)
                fe.Create(MediaFormatManager.programName);
            else                  
                fe.ProgramName = programName;

            SystemNotification.RegistrSettingsIsChanged();
        }

        /// <summary>
        /// Unassign reference to out program
        /// </summary>
        /// <param name="extension">For example ".avi"</param>
        public static void UnAssign(string extension)
        {
            FileExtension fe = new FileExtension(extension);
            if (fe.Exists)
                fe.ProgramName = "";

            SystemNotification.RegistrSettingsIsChanged();
        }
    }
}
