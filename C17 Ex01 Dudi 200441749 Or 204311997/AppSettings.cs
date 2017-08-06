using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public class AppSettings
    {
        private static string m_Path = "AppSettings.xml";
        private static AppSettings m_Instance;
        public Point LastWindowsLocation { get; set; }
        public Size LastWindowsSize { get; set; }
        public bool RememberUser { get; set; }
        //public User LoginUser { get; set; }
        public string LastAccessToken { get; set; }
        

        private AppSettings()
        {
        }

        public static AppSettings Instance
        {
            get
            {
                // TODO lock ?
                if (m_Instance == null)
                {
                    m_Instance = new AppSettings();
                }

                return m_Instance;
            }
            private set
            {
                m_Instance = value;
            }
        }

        public void SaveToFile()
        {
            deleteSettingsFile();
            using (Stream stream = new FileStream(m_Path, FileMode.CreateNew))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stream, this);
            }
        }

        private void deleteSettingsFile()
        {
            try
            {
                if (File.Exists(m_Path))
                {
                    File.Delete(m_Path);
                }
            }
            catch
            {
                throw new Exception("Fail when try to delete the old settings file");
            }
        }

        public static AppSettings LoadFromFile()
        {
            AppSettings appSettings = null;
            Stream stream = null;

            if (File.Exists(m_Path))
            {
                try
                {
                    stream = new FileStream(m_Path, FileMode.Open);
                    XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                    appSettings = serializer.Deserialize(stream) as AppSettings;
                    m_Instance = appSettings;
                }
                catch(Exception e)
                {
                    throw new FileLoadException("Fail when tried to load " + m_Path + " file");
                }
                finally
                {
                    stream.Dispose();
                }
            }

            return appSettings;
        }

        public void Clear()
        {
            deleteSettingsFile();
        }

        public void DefaultSettings(FormMain i_Form)
        {
            LastWindowsLocation = i_Form.Location;
            LastWindowsSize = i_Form.Size;
            RememberUser = i_Form.RememberMe;
        }
    }
}
