﻿using FacebookWrapper.ObjectModel;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public class AppSettings
    {
        private const string k_SettingsFilePath = "AppSettings.xml";
        //TODO set the default size after done with design
        private static readonly Size sr_DefaultFormSize = new Size(1394, 867);
        private static readonly object sr_CreationLock = new object();
        public Point LastWindowLocation { get; set; }
        public Size LastWindowsSize { get; set; }
        public FormStartPosition LastFormStartPosition { get; set; }
        public bool RememberUser { get; set; }
        public string LastAccessToken { get; set; }

        private AppSettings()
        {
            SetDefaultSettings();
        }

        public void SaveToFile()
        {
            // TODO is this the right way to to this?
            if (!File.Exists(k_SettingsFilePath))
            {
                FileStream tempFile = File.Create(k_SettingsFilePath);
                tempFile.Dispose();
            }

            using (Stream stream = new FileStream(k_SettingsFilePath, FileMode.Truncate))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stream, this);
            }
        }

        public static AppSettings LoadFromFile()
        {
            AppSettings appSettings = null;
            Stream stream = null;

            try
            {
                stream = new FileStream(k_SettingsFilePath, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                appSettings = serializer.Deserialize(stream) as AppSettings;
            }
            catch
            {
                appSettings = new AppSettings();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }

            return appSettings;
        }

        public void SetDefaultSettings()
        {            
            LastWindowsSize = sr_DefaultFormSize;
            LastFormStartPosition = FormStartPosition.CenterScreen;
            LastWindowLocation = new Point(0,0);
            LastAccessToken = null;
            RememberUser = false;
        }
    }
}
