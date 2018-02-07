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
    /// All our used format group
    /// </summary>
    public static class MediaFormatGroups
    {
        #region Field

        static MediaFormatGroup asf = new MediaFormatGroup(MediaFormats.Asf, MediaFormats.Asx, /*MediaFormats.DvrMs,*/ MediaFormats.Wpl, MediaFormats.Wm, MediaFormats.Wmx, MediaFormats.Wmd, MediaFormats.Wmz);

        static MediaFormatGroup wma = new MediaFormatGroup(MediaFormats.Wma, MediaFormats.Wax);

        static MediaFormatGroup wmv = new MediaFormatGroup(MediaFormats.Wmv, MediaFormats.Wvx);

        static MediaFormatGroup cda = new MediaFormatGroup(MediaFormats.Cda);

        static MediaFormatGroup avi = new MediaFormatGroup(MediaFormats.Avi);

        static MediaFormatGroup wav = new MediaFormatGroup(MediaFormats.Wav);

        static MediaFormatGroup mpeg = new MediaFormatGroup(MediaFormats.Mpeg, MediaFormats.Mpg, MediaFormats.Mpe, MediaFormats.M1v, MediaFormats.Mp2, MediaFormats.Mpv2, MediaFormats.Mp2v, MediaFormats.Mpa);

        static MediaFormatGroup mp3 = new MediaFormatGroup(MediaFormats.Mp3, MediaFormats.M3u);

        static MediaFormatGroup midi = new MediaFormatGroup(MediaFormats.Mid, MediaFormats.Midi, MediaFormats.Rmi);

        static MediaFormatGroup aiff = new MediaFormatGroup(MediaFormats.Aif, MediaFormats.Aifc, MediaFormats.Aiff);

        static MediaFormatGroup au = new MediaFormatGroup(MediaFormats.Au, MediaFormats.Snd);

        static MediaFormatGroup mp4a = new MediaFormatGroup(MediaFormats.M4a, MediaFormats.M4b);
        
        static MediaFormatGroup mp4v = new MediaFormatGroup(MediaFormats.Mp4, MediaFormats.M4v, MediaFormats.Mp4v);
        
        static MediaFormatGroup mka = new MediaFormatGroup(MediaFormats.Mka);
        
        static MediaFormatGroup mkv = new MediaFormatGroup(MediaFormats.Mkv);

        static MediaFormatGroup flv = new MediaFormatGroup(MediaFormats.Flv);

        static MediaFormatGroup myFormat = new MediaFormatGroup(MediaFormats.Wm, MediaFormats.Asf, MediaFormats.Mumu, MediaFormats.Wow);        

        /// <summary>
        /// Path to our exe file on file system.
        /// </summary>
        static string programPath;

        #endregion 

        #region Properties

        public static MediaFormatGroup Asf
        {
            get { return MediaFormatGroups.asf; }
            set { MediaFormatGroups.asf = value; }
        }        

        public static MediaFormatGroup Wma
        {
            get { return MediaFormatGroups.wma; }
            set { MediaFormatGroups.wma = value; }
        }        

        public static MediaFormatGroup Wmv
        {
            get { return MediaFormatGroups.wmv; }
            set { MediaFormatGroups.wmv = value; }
        }        

        public static MediaFormatGroup Cda
        {
            get { return MediaFormatGroups.cda; }
            set { MediaFormatGroups.cda = value; }
        }        

        public static MediaFormatGroup Avi
        {
            get { return MediaFormatGroups.avi; }
            set { MediaFormatGroups.avi = value; }
        }        

        public static MediaFormatGroup Wav
        {
            get { return MediaFormatGroups.wav; }
            set { MediaFormatGroups.wav = value; }
        }        

        public static MediaFormatGroup Mpeg
        {
            get { return MediaFormatGroups.mpeg; }
            set { MediaFormatGroups.mpeg = value; }
        }        

        public static MediaFormatGroup Mp3
        {
            get { return MediaFormatGroups.mp3; }
            set { MediaFormatGroups.mp3 = value; }
        }        

        public static MediaFormatGroup Midi
        {
            get { return MediaFormatGroups.midi; }
            set { MediaFormatGroups.midi = value; }
        }        

        public static MediaFormatGroup Aiff
        {
            get { return MediaFormatGroups.aiff; }
            set { MediaFormatGroups.aiff = value; }
        }        

        public static MediaFormatGroup Au
        {
            get { return MediaFormatGroups.au; }
            set { MediaFormatGroups.au = value; }
        }

        public static MediaFormatGroup Mp4a
        {
            get { return MediaFormatGroups.mp4a; }
            set { MediaFormatGroups.mp4a = value; }
        }

        public static MediaFormatGroup Mp4v
        {
            get { return MediaFormatGroups.mp4v; }
            set { MediaFormatGroups.mp4v = value; }
        }

        public static MediaFormatGroup Mkv
        {
            get { return MediaFormatGroups.mkv; }
            set { MediaFormatGroups.mkv = value; }
        }

        public static MediaFormatGroup Mka
        {
            get { return MediaFormatGroups.mka; }
            set { MediaFormatGroups.mka = value; }
        }

        public static MediaFormatGroup Flv
        {
            get { return MediaFormatGroups.flv; }
            set { MediaFormatGroups.flv = value; }
        }       

        public static MediaFormatGroup MyFormat
        {
            get { return MediaFormatGroups.myFormat; }
            set { MediaFormatGroups.myFormat = value; }
        }       

        public static string ProgramPath
        {
            get { return MediaFormatGroups.programPath; }
            set 
            { 
                MediaFormatGroups.programPath = value;
                MediaFormatManager.CreateProgram(value);
            }
        }

        public static List<string> AllVideoExtensions
        {
            get
            {
                return GetAllExtensions(new List<MediaFormatGroup> { asf, wmv, cda, avi, mpeg, aiff, au, mkv, flv, mp4v });
            }        
        }

        public static List<string> AllAudioExtensions
        {
            get
            {
                return GetAllExtensions(new List<MediaFormatGroup>{wma, mp3, midi, wav, mka, mp4a});
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Remove our program from registry.
        /// </summary>
        public static void ClearAllAssociation()
        {
            MediaFormatManager.ClearAllAssociation();
        }

        /// <summary>
        /// Get all format extension list for all groups
        /// </summary>        
        static List<string> GetAllExtensions(List<MediaFormatGroup> groups)
        {
            List<string> allFormats = new List<string>();

            foreach (MediaFormatGroup group in groups)
                allFormats.AddRange(group.Formats);

            return allFormats;
        }

        #endregion
    }
}
