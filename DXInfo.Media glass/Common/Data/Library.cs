#region License

//Media glass - my simple WPF player
//Copyright (C) 2008-2009 Denis Yakimenko <denyakimenko@yandex.ru>

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
using System.Windows.Controls;
using System.Xml;
using System.IO;
using System.Reflection;
using Media_glass.Common.Enums;
using Media_glass.Common.Items_operations;

namespace Media_glass.Common.Data.bak
{
    /// <summary>
    /// Read or write data to xml library file.
    /// </summary>
    class Library
    {
        #region Variables

        //Library.xml sample.
        
        //<?xml version="1.0" encoding="utf-16"?>
        //<Library>
        //  <Group Name="Music " IsExpanded="True">
        //    <PlayList Name="Pussy cat dolls" File="633774952326406250.xml" />
        //    <PlayList Name="50 cent" File="633774957915000000.xml" />
        //    <PlayList Name="Beyonce" File="633774967447343750.xml" />
        //    <PlayList Name="Christina Aguilera" File="633774977067968750.xml" />
        //    <PlayList Name="Avril Lavigne" File="633774985873906250.xml" />
        //    <PlayList Name="My favourite artist" File="633774990966093750.xml" />
        //  </Group>
        //  <Group Name="Music Video" IsExpanded="True">
        //    <PlayList Name="Pussy cat dolls" File="633774949007031250.xml" />
        //    <PlayList Name="50 cent" File="633789789084687500.xml" />
        //    <PlayList Name="Beyonce" File="633789789284218750.xml" />
        //    <PlayList Name="Christina Aguilera" File="633789789347968750.xml" />
        //    <PlayList Name="Avril Lavigne" File="633789789417031250.xml" />
        //  </Group>
        //  <Group Name="Video" IsExpanded="True">
        //    <PlayList Name="Pirates of the Caribbean: The Curse of the Black Pearl" File="633789789617968750.xml" />
        //  </Group>
        //</Library>

        //633774952326406250.xml play list file sample.

        //<?xml version="1.0" encoding="utf-16"?>
        //<PlayList>
        //  <Media Name="Mick Jagger Feat Lenny Kravitz - God Gave Me Everything.mpg" File="C:\A MUSIC\b music video.rock\Mick Jagger Feat Lenny Kravitz - God Gave Me Everything.mpg" />
        //  <Media Name="Avril Lavigne - Girlfriend Live in Sunrise.avi" File="C:\A MUSIC\b music video.rock\Avril Lavigne - Girlfriend Live in Sunrise.avi" />  
        //</PlayList>

        /// <summary>
        /// Library root xml element.
        /// </summary>
        const string libraryTagName = "Library";

        /// <summary>
        /// Library version attribute.
        /// </summary>
        const string libraryVersionAttributeName = "FormatVersion";
        
        /// <summary>
        /// Group element in .xml library.
        /// </summary>
        const string groupTagName = "Group";

        /// <summary>
        /// Group name attribute.
        /// </summary>
        const string groupNameAttributeName = "Name";

        /// <summary>
        /// Is group expanded or not. Group attribute.
        /// </summary>
        const string groupExpandedAttributeName = "IsExpanded";        

        /// <summary>
        /// Play list element library name tag.
        /// </summary>
        const string listTagName = "PlayList";

        /// <summary>
        /// Play list name attribute.
        /// </summary>
        const string listNameAttributeName = "Name";

        /// <summary>
        /// Play list file name attribute.
        /// </summary>
        const string listFileAttributeName = "File";

        /// <summary>
        /// Media element in the play list.
        /// </summary>
        const string mediaTagName = "Media";

        /// <summary>
        /// Media name attribute.
        /// </summary>
        const string mediaNameAttributeName = "Name";

        /// <summary>
        /// Media file name attribute.
        /// </summary>
        const string mediaFileAttributeName = "File";

        /// <summary>
        /// Media file name attribute.
        /// </summary>
        const string mediaTimeAttributeName = "Time";

        /// <summary>
        /// Media file name attribute.
        /// </summary>
        const string mediaTypeAttributeName = "MediaType";

        /// <summary>
        /// Media file name attribute.
        /// </summary>
        const string mediaResolutionAttributeName = "Resolution";  

        /// <summary>
        /// Path to library directory.
        /// </summary>
        readonly string libraryDirectory;

        /// <summary>
        /// Path to library directory.
        /// </summary>
        public string LibraryDirectory
        {
            get { return libraryDirectory; }
        } 

        /// <summary>
        /// Path to library directory + library name.
        /// </summary>
        readonly string libraryPath;

        /// <summary>
        /// Current library structure version.
        /// </summary>
        const LibraryStructureVersion ApplicationLibraryStructureVersion = LibraryStructureVersion.ReleaseCandidate1;

        #endregion

        /// <summary>
        /// Constructor -  initializes the instance of window.
        /// </summary>
        public Library() : this(System.IO.Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)/*Directory.GetCurrentDirectory()*//*AppDomain.CurrentDomain.BaseDirectory*/, @"Library\"))
        {        
        }

        /// <summary>
        /// Constructor -  initializes the instance of window.
        /// </summary>
        public Library(string libraryDirectory)
        {
            this.libraryDirectory = libraryDirectory;
            this.libraryPath = System.IO.Path.Combine(libraryDirectory, "Library.xml");

            string directory = libraryDirectory;

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(this.libraryPath))
            {
                TreeView tv = new TreeView();
                tv.Items.Add(new TreeViewItem() { Header = "Music" });
                tv.Items.Add(new TreeViewItem() { Header = "Music Video" });
                tv.Items.Add(new TreeViewItem() { Header = "Movies" });

                this.SaveLibrary(tv.Items);
            }

            UpdateIfNeeded();
        }

        #region Update xml library structure if needed

        /// <summary>
        /// Library structure vesrion 
        /// </summary>
        enum LibraryStructureVersion
        {
            Undefined = -1,
            Beta1 = 0,
            Beta2 = 1,
            ReleaseCandidate1 = 2
        }                

        /// <summary>
        /// Get current library version.
        /// </summary>        
        LibraryStructureVersion GetCurrentLibraryVersion()
        {
            try
            {
                XmlDocument xmlLibrary = new XmlDocument();
                xmlLibrary.Load(this.libraryPath);

                XmlNode node = xmlLibrary.DocumentElement;

                foreach (XmlAttribute attribute in node.Attributes)
                    if (attribute.Name == libraryVersionAttributeName)
                        return (LibraryStructureVersion)int.Parse(attribute.Value);

                return LibraryStructureVersion.Undefined;
            }
            catch
            {
                return LibraryStructureVersion.Undefined;
            }
        }

        /// <summary>
        /// Set current library version.
        /// </summary>        
        void SetLibraryVersion(LibraryStructureVersion version)
        {
            XmlDocument xmlLibrary = new XmlDocument();
            xmlLibrary.Load(this.libraryPath);

            XmlNode node = xmlLibrary.DocumentElement;

            foreach (XmlAttribute attribute in node.Attributes)
                if (attribute.Name == libraryVersionAttributeName)
                {
                    attribute.Value = ((int)version).ToString();
                    xmlLibrary.Save(this.libraryPath);
                    return;
                }

            XmlAttribute versionAttribute = xmlLibrary.CreateAttribute(Library.libraryVersionAttributeName);
            versionAttribute.Value = ((int)version).ToString();
            node.Attributes.Append(versionAttribute);

            xmlLibrary.Save(this.libraryPath);
        }

        /// <summary>
        /// Update play list to beta 2 format.
        /// </summary>        
        void UpdatePlayListToBeta2LibraryFormat(string playListFileName)
        {
            if (!File.Exists(this.GetPlayListPath(playListFileName)))
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load(this.GetPlayListPath(playListFileName));

            XmlNodeList nodes = doc.GetElementsByTagName(Library.mediaTagName);

            foreach (XmlNode node in nodes)
            {
                //if time attribute for media element already exists then nothing to do.
                foreach (XmlAttribute attribute in node.Attributes)
                    if (attribute.Name == Library.mediaTimeAttributeName)
                        return;

                //otherwise add it.
                XmlAttribute timeAttribute = doc.CreateAttribute(Library.mediaTimeAttributeName);
                timeAttribute.Value = null;
                node.Attributes.Append(timeAttribute);
            }

            doc.Save(this.GetPlayListPath(playListFileName));
        }

        /// <summary>
        /// Update play list to Release candidate 1 format.
        /// </summary>        
        void UpdatePlayListToRC1LibraryFormat(string playListFileName)
        {
            if (!File.Exists(this.GetPlayListPath(playListFileName)))
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load(this.GetPlayListPath(playListFileName));

            XmlNodeList nodes = doc.GetElementsByTagName(Library.mediaTagName);

            //Add new attribute MediaType
            foreach (XmlNode node in nodes)
            {
                //if time attribute for media element already exists then nothing to do.
                foreach (XmlAttribute attribute in node.Attributes)
                    if (attribute.Name == Library.mediaTypeAttributeName)
                        break;

                //otherwise add it.
                XmlAttribute typeAttribute = doc.CreateAttribute(Library.mediaTypeAttributeName);
                typeAttribute.Value = null;
                node.Attributes.Append(typeAttribute);
            }

            //Add new attribute Resolution
            foreach (XmlNode node in nodes)
            {
                //if time attribute for media element already exists then nothing to do.
                foreach (XmlAttribute attribute in node.Attributes)
                    if (attribute.Name == Library.mediaResolutionAttributeName)
                        break;

                //otherwise add it.
                XmlAttribute resolutionAttribute = doc.CreateAttribute(Library.mediaResolutionAttributeName);
                resolutionAttribute.Value = null;
                node.Attributes.Append(resolutionAttribute);
            }

            doc.Save(this.GetPlayListPath(playListFileName));
        }

        /// <summary>
        /// Update library if needed.
        /// </summary>
        void UpdateIfNeeded()
        {
            if (GetCurrentLibraryVersion() < Library.ApplicationLibraryStructureVersion)
            {
                //Update previous library format to beta2 xml library format.

                if (GetCurrentLibraryVersion() < LibraryStructureVersion.Beta2)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(this.libraryPath);

                    XmlNodeList nodes = doc.GetElementsByTagName(Library.groupTagName);

                    foreach (XmlNode node in nodes)
                        foreach (XmlNode childNode in node.ChildNodes)
                            UpdatePlayListToBeta2LibraryFormat(childNode.Attributes[Library.listFileAttributeName].Value);

                    //System.Windows.Forms.MessageBox.Show("Update");
                }

                //Update previous library format to release candidate 1 xml library format.

                if (GetCurrentLibraryVersion() < LibraryStructureVersion.ReleaseCandidate1)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(this.libraryPath);

                    XmlNodeList nodes = doc.GetElementsByTagName(Library.groupTagName);

                    foreach (XmlNode node in nodes)
                        foreach (XmlNode childNode in node.ChildNodes)
                            UpdatePlayListToRC1LibraryFormat(childNode.Attributes[Library.listFileAttributeName].Value);                    
                }

                //Update beta 2 library format to new xml library format.

                //if (GetCurrentLibraryVersion() < LibraryStructureVersion.NewFormat)
                //{
                //    ... do somtheting here 
                //}

                //Update library version.

                SetLibraryVersion(Library.ApplicationLibraryStructureVersion);
            }
        }

        #endregion

        /// <summary>
        /// Check is library has right format
        /// </summary>        
        public static bool TryToOpen(string libraryPath)
        {
            try
            {
                //Check if main library file exists
                string libraryRootPath = System.IO.Path.Combine(libraryPath, "Library.xml");

                if (!File.Exists(libraryRootPath))
                    throw new Exception();

                //Check main library file
                System.Windows.Controls.TreeView tv = new System.Windows.Controls.TreeView();
                ItemCollection items = tv.Items;
                Library library = new Library(libraryPath);
                library.LoadLibrary(items);

                //Get all playlist files
                var playlists = new List<string>();

                foreach (TreeViewItem tvi in items)
                    foreach (TreeViewItem child in tvi.Items)
                        playlists.Add(child.Tag as string);

                //Check all playlist
                System.Windows.Controls.ListView lv = new System.Windows.Controls.ListView();
                foreach (var playlist in playlists)
                    library.LoadPlayList(lv.Items, playlist);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get full path to the xml play list file.
        /// </summary>
        /// <param name="fileName">xml play list file name.</param>        
        string GetPlayListPath(string fileName)
        {
            return System.IO.Path.Combine(libraryDirectory, fileName);
        }

        /// <summary>
        /// Save item collection to the .xml library.
        /// </summary>
        /// <param name="items"></param>
        public void SaveLibrary(ItemCollection items)
        {
            XmlTextWriter writer = null;
            try
            {
                writer = new XmlTextWriter(this.libraryPath, System.Text.Encoding.Unicode);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement(Library.libraryTagName);

                foreach (TreeViewItem tvi in items)
                {
                    writer.WriteStartElement(Library.groupTagName);
                    writer.WriteAttributeString(Library.listNameAttributeName, tvi.Header.ToString());
                    writer.WriteAttributeString(Library.groupExpandedAttributeName, tvi.IsExpanded.ToString());

                    foreach (TreeViewItem childTvi in tvi.Items)
                    {
                        writer.WriteStartElement(Library.listTagName);
                        writer.WriteAttributeString(Library.listNameAttributeName, childTvi.Header.ToString());
                        writer.WriteAttributeString(Library.listFileAttributeName, childTvi.Tag as string);
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();                
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            };

            SetLibraryVersion(Library.ApplicationLibraryStructureVersion);
        }

        /// <summary>
        /// Load group and theri play lists from xml to tree view item collection.
        /// </summary>        
        public void LoadLibrary(ItemCollection items)
        {
            if (!File.Exists(this.libraryPath))
                return;

            items.Clear();

            XmlDocument doc = new XmlDocument();
            doc.Load(this.libraryPath);

            XmlNodeList nodes = doc.GetElementsByTagName(Library.groupTagName);            

            foreach (XmlNode node in nodes)
            {
                TreeViewItem tvi = new TreeViewItem();
                tvi.Header = node.Attributes[Library.groupNameAttributeName].Value;
                tvi.IsExpanded = bool.Parse(node.Attributes[Library.groupExpandedAttributeName].Value);

                foreach (XmlNode childNode in node.ChildNodes)
                {
                    TreeViewItem childTvi = new TreeViewItem();

                    childTvi.Header = childNode.Attributes[Library.listNameAttributeName].Value;
                    childTvi.Tag = childNode.Attributes[Library.listFileAttributeName].Value;

                    tvi.Items.Add(childTvi);
                }

                items.Add(tvi);
            }
        }

        /// <summary>
        /// Get all group list.
        /// </summary>        
        public List<string> GetGroups()
        {
            List<string> groups = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(this.libraryPath);

            XmlNodeList nodes = doc.GetElementsByTagName(Library.groupTagName);

            foreach (XmlNode node in nodes)
                groups.Add(node.Attributes[Library.groupNameAttributeName].Value);

            return groups;
        }

        /// <summary>
        /// Add new play list info to xml library. We add only play list name and play list file name. 
        /// And we don't add it content to it.
        /// </summary>
        /// <param name="groupIndex">play list group index</param>
        /// <param name="playListName">play list name</param>
        /// <param name="fileName">play list xml file name</param>
        /// <returns>play list index</returns>
        public int AddPlayListToLibrary(int groupIndex, string playListName, string fileName)
        {
            List<string> groups = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(this.libraryPath);

            XmlNodeList nodes = doc.GetElementsByTagName(Library.groupTagName);

            XmlElement newNode = doc.CreateElement(Library.listTagName);
            XmlAttribute newAttribute = doc.CreateAttribute(Library.listNameAttributeName);
            newAttribute.Value = playListName;
            newNode.Attributes.Append(newAttribute);

            newAttribute = doc.CreateAttribute(Library.listFileAttributeName);
            newAttribute.Value = fileName;
            newNode.Attributes.Append(newAttribute);              
                
            nodes[groupIndex].AppendChild(newNode);
            doc.Save(this.libraryPath);

            return nodes[groupIndex].ChildNodes.Count - 1; 
        }

        /// <summary>
        /// Save play list item collection to xml file.
        /// </summary>        
        public void SavePlayList(ItemCollection items, string playListFileName)
        {
            XmlTextWriter writer = null;
            try
            {
                writer = new XmlTextWriter(GetPlayListPath(playListFileName), System.Text.Encoding.Unicode);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement(Library.listTagName);

                foreach (ListViewItem lvi in items)
                {
                    writer.WriteStartElement(Library.mediaTagName);
                    
                    writer.WriteAttributeString(Library.mediaNameAttributeName, ((ItemContent)lvi.Content).Name);                    
                    writer.WriteAttributeString(Library.mediaFileAttributeName, lvi.Tag as string);
                    writer.WriteAttributeString(Library.mediaTimeAttributeName, ((ItemContent)lvi.Content).Time);
                    writer.WriteAttributeString(Library.mediaTypeAttributeName, ((ItemContent)lvi.Content).MediaType.ToString());
                    writer.WriteAttributeString(Library.mediaResolutionAttributeName, ((ItemContent)lvi.Content).Resolution);
                    
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            };
        }

        /// <summary>
        /// Load play list from xml.
        /// </summary>        
        public void LoadPlayList(ItemCollection items, string playListFileName)
        {
            items.Clear();

            if (!File.Exists(this.GetPlayListPath(playListFileName)))
                return;

            //items.Clear();

            XmlDocument doc = new XmlDocument();
            doc.Load(this.GetPlayListPath(playListFileName));

            XmlNodeList nodes = doc.GetElementsByTagName(Library.mediaTagName);

            foreach (XmlNode node in nodes)
            {
                ListViewItem lvi = new ListViewItem();
                ItemContent itemContent = new ItemContent();
                itemContent.Name = node.Attributes[Library.mediaNameAttributeName].Value;
                itemContent.Time = node.Attributes[Library.mediaTimeAttributeName].Value;

                string mediaType = node.Attributes[Library.mediaTypeAttributeName].Value;
                if(String.IsNullOrEmpty(mediaType))
                    itemContent.MediaType = MediaType.NotPlayed;
                else
                    itemContent.MediaType = (MediaType)Enum.Parse(typeof(MediaType),mediaType);

                itemContent.Resolution = node.Attributes[Library.mediaResolutionAttributeName].Value;
                lvi.Content = itemContent;                 
                lvi.Tag = node.Attributes[Library.mediaFileAttributeName].Value;                

                items.Add(lvi);
            }
        }

        /// <summary>
        /// Rename play list in .xml library.
        /// </summary>        
        public void RenamePlayList(string playListFileName, string newName)
        {
            if (!File.Exists(this.libraryPath))
                return;            

            XmlDocument doc = new XmlDocument();
            doc.Load(this.libraryPath);

            XmlNodeList nodes = doc.GetElementsByTagName(Library.groupTagName);

            foreach (XmlNode node in nodes)
            {           
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Attributes[Library.listFileAttributeName].Value == playListFileName)
                        childNode.Attributes[Library.listNameAttributeName].Value = newName;

                    doc.Save(this.libraryPath);

                    return;
                }            
            }
        }

        /// <summary>
        /// Delete play list from disk.
        /// </summary>        
        public void DeletePlayList(string playListFileName)
        {
            File.Delete(GetPlayListPath(playListFileName));
        }

        /// <summary>
        /// Get play list file name from xml library file. 
        /// </summary>
        /// <param name="groupIndex">Group index.</param>
        /// <param name="listIndex">Play list index in the group.</param>
        /// <returns></returns>
        public string GetPlayListFileName(int groupIndex, int listIndex)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.libraryPath);

            XmlNodeList groupNodes = doc.GetElementsByTagName(Library.groupTagName);

            return groupNodes[groupIndex].ChildNodes[listIndex].Attributes[Library.listFileAttributeName].Value.ToString();
        }

        /// <summary>
        /// Get play list file name from xml library file. 
        /// </summary>
        /// <param name="groupIndex">Group index.</param>
        /// <param name="listIndex">Play list index in the group.</param>
        /// <returns></returns>
        public string GetPlayListName(int groupIndex, int listIndex)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.libraryPath);

            XmlNodeList groupNodes = doc.GetElementsByTagName(Library.groupTagName);

            return groupNodes[groupIndex].ChildNodes[listIndex].Attributes[Library.listNameAttributeName].Value.ToString();
        }
    }
}
