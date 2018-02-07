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
    /// All music / video extensions
    /// </summary>
    static class MediaFormats 
    {
        #region ASF

        public static readonly string Asf = ".asf";
        public static readonly string Asx = ".asx";
        //public static readonly string DvrMs = ".dvr-ms";
        public static readonly string Wpl = ".wpl";
        public static readonly string Wm = ".wm";
        public static readonly string Wmx = ".wmx";
        public static readonly string Wmd = ".wmd";
        public static readonly string Wmz = ".wmz";        

        #endregion 

        #region WMA

        public static readonly string Wma = ".wma";
        public static readonly string Wax = ".wax";        

        #endregion

        #region WMV

        public static readonly string Wmv = ".wmv";
        public static readonly string Wvx = ".wvx";        

        #endregion

        #region CDA

        public static readonly string Cda = ".cda";

        #endregion

        #region AVI

        public static readonly string Avi = ".avi";

        #endregion

        #region WAV

        public static readonly string Wav = ".wav";

        #endregion

        #region MPEG

        public static readonly string Mpeg = ".mpeg";
        public static readonly string Mpg = ".mpg";
        public static readonly string Mpe = ".mpe";
        public static readonly string M1v = ".m1v";
        public static readonly string Mp2 = ".mp2";
        public static readonly string Mpv2 = ".mpv2";
        public static readonly string Mp2v = ".mp2v";
        public static readonly string Mpa = ".mpa";

        #endregion

        #region MP3

        public static readonly string Mp3 = ".mp3";
        public static readonly string M3u = ".m3u";

        #endregion

        #region MIDI

        public static readonly string Mid = ".mid";
        public static readonly string Midi = ".midi";
        public static readonly string Rmi = ".rmi";        

        #endregion

        #region AIFF

        public static readonly string Aif = ".aif";
        public static readonly string Aifc = ".aifc";
        public static readonly string Aiff = ".aiff";

        #endregion 

        #region AU

        public static readonly string Au =".au";
        public static readonly string Snd = ".snd";

        #endregion

        #region MPEG-4 Audio

        public static readonly string M4a = ".m4a";
        public static readonly string M4b = ".m4b";

        #endregion

        #region MPEG-4 Video

        public static readonly string Mp4 = ".mp4";
        public static readonly string M4v = ".m4v";
        public static readonly string Mp4v = ".mp4v";

        #endregion

        #region Matroska Audio

        public static readonly string Mka = ".mka";        

        #endregion

        #region Matroska Video

        public static readonly string Mkv = ".mkv";

        #endregion

        #region Flash Video
        
        public static readonly string Flv = ".flv";

        #endregion

        #region Test format

        public static readonly string Wow = ".wow2";
        public static readonly string Mumu = ".mumumu";

        #endregion
    }
}
