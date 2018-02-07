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
    /// Help to test Error Window.
    /// </summary>
    public class ExceptionGenerator
    {
        class SecondLevelException : Exception
        {
            public SecondLevelException(string message, Exception inner)
                : base(message, inner)
            { }
        }
        class ThirdLevelException : Exception
        {
            public ThirdLevelException(string message, Exception inner)
                : base(message, inner)
            { }
        }
         
        /// <summary>
        /// This function catches the exception from the called 
        /// function DivideBy0( ) and throws another in response.
        /// </summary>
        public static void MakeException()
        {
            try
            {
                DivideBy0();
            }
            catch (Exception ex)
            {
                throw new ThirdLevelException(
                    "Caught the second exception and " +
                    "threw a third in response.", ex);
            }
        }
        
        /// <summary>
        /// This function forces a division by 0 and throws a second 
        /// exception.
        /// </summary>
        static void DivideBy0()
        {
            try
            {
                int zero = 0;
                int ecks = 1 / zero;
            }
            catch (Exception ex)
            {
                throw new SecondLevelException(
                    "Forced a division by 0 and threw " +
                    "a second exception.", ex);
            }
        }
    }
}
