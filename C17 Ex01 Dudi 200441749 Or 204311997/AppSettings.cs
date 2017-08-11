using FacebookWrapper.ObjectModel;
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
        private static readonly object sr_CreationLock = new object();
        private static AppSettings s_Instance;
        public Point LastWindowsLocation { get; set; }
        public Size LastWindowsSize { get; set; }
        public bool RememberUser { get; set; }
        public string LastAccessToken { get; set; }
        //public User LoginUser { get; set; }

        private AppSettings()
        {
            SetDefaultSettings();
        }

        private Point getDefaultStartingPointForMainForm()
        {
            int x = Screen.PrimaryScreen.WorkingArea.Width / 2;
            int y = Screen.PrimaryScreen.WorkingArea.Height / 2;
            return new Point(x - (LastWindowsSize.Width) / 2, y - (LastWindowsSize.Height) / 2);
        }

        public static AppSettings Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (sr_CreationLock)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = new AppSettings();
                        }
                    }
                }

                return s_Instance;
            }
            // TODO singletons don't have set (it happens in the get) - Delete after reading
            /*
            private set
            {
                s_Instance = value;
            } 
            */
        }

        public void SaveToFile()
        {
            //deleteSettingsFile();
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
            catch (Exception e)
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

        //private void deleteSettingsFile()
        //{
        //    try
        //    {
        //        if (File.Exists(m_Path))
        //        {
        //            File.Delete(m_Path);
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("Fail when try to delete the old settings file");
        //    }
        //}

        //public void Clear()
        //{
        //    deleteSettingsFile();
        //}

        public void SetDefaultSettings()
        {
            //TODO isn't the initial size to big?
            LastWindowsSize = new Size(1394, 867);
            LastWindowsLocation = getDefaultStartingPointForMainForm();
            LastAccessToken = null;
            RememberUser = false;
        }
    }
}
