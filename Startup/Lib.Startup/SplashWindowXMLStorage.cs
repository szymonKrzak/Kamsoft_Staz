using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.IO.IsolatedStorage;

namespace Lib.Startup
{
    /// <summary>
    /// A specialized class for managing XML storage for the splash screen.
    /// </summary>
    internal class SplashWindowXMLStorage
    {
        private string _fileName = "SplashScreen.xml";
        private string _defaultCheckPoints = string.Empty;
        private string _defaultIncrement = ".015";

        public SplashWindowXMLStorage(string appName)
        {
            _fileName = string.Format("{0}.xml", appName);
        }

        /// <summary>
        /// String z podziałem procentowym czasu uruchomienia aplikacji pomiędzy checkPointy
        /// </summary>
        public string CheckPoints
        {
            get { return GetValue("CheckPoints", _defaultCheckPoints); }
            set { SetValue("CheckPoints", value); }
        }

        /// <summary>
        /// Interwał czasu pomiędzy poszczególnymi checkPointami
        /// </summary>
        public string Interval
        {
            get { return GetValue("Interval", _defaultIncrement); }
            set { SetValue("Interval", value); }
        }

        // Store the file in a location where it can be written with only User rights. (Don't use install directory).
        private string StoragePath
        {
            get { return Path.Combine(Application.UserAppDataPath, _fileName); }
        }

        //// Helper method for getting inner text of named element.
        //private string GetValue(string name, string defaultValue)
        //{
        //    if (!File.Exists(StoragePath))
        //        return defaultValue;

        //    try
        //    {
        //        XmlDocument docXML = new XmlDocument();
        //        docXML.Load(StoragePath);
        //        XmlElement elValue = docXML.DocumentElement.SelectSingleNode(name) as XmlElement;
        //        return (elValue == null) ? defaultValue : elValue.InnerText;
        //    }
        //    catch
        //    {
        //        return defaultValue;
        //    }
        //}

        //// Helper method for setting inner text of named element.  Creates document if it doesn't exist.
        //public void SetValue(string name, string stringValue)
        //{
        //    XmlDocument docXML = new XmlDocument();
        //    XmlElement elRoot = null;
        //    if (!File.Exists(StoragePath))
        //    {
        //        elRoot = docXML.CreateElement("root");
        //        docXML.AppendChild(elRoot);
        //    }
        //    else
        //    {
        //        docXML.Load(StoragePath);
        //        elRoot = docXML.DocumentElement;
        //    }
        //    XmlElement value = docXML.DocumentElement.SelectSingleNode(name) as XmlElement;
        //    if (value == null)
        //    {
        //        value = docXML.CreateElement(name);
        //        elRoot.AppendChild(value);
        //    }
        //    value.InnerText = stringValue;
        //    docXML.Save(StoragePath);
        //}

        // Helper method for getting inner text of named element.
        private string GetValue(string name, string defaultValue)
        {
            try
            {
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    if (!myIsolatedStorage.FileExists(_fileName))
                        return defaultValue;

                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(_fileName, FileMode.Open))
                    {
                        XmlDocument docXML = new XmlDocument();
                        docXML.Load(stream);
                        XmlElement elValue = docXML.DocumentElement.SelectSingleNode(name) as XmlElement;
                        return (elValue == null) ? defaultValue : elValue.InnerText;
                    }
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        // Helper method for setting inner text of named element.  Creates document if it doesn't exist.
        public void SetValue(string name, string stringValue)
        {
            XmlDocument docXML = new XmlDocument();
            XmlElement elRoot = null;

            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                if (!myIsolatedStorage.FileExists(_fileName))
                {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(_fileName, FileMode.CreateNew))
                    {
                        elRoot = docXML.CreateElement("root");
                        docXML.AppendChild(elRoot);
                        XmlElement value = docXML.CreateElement(name);
                        elRoot.AppendChild(value);
                        value.InnerText = stringValue;
                        docXML.Save(stream);
                    }
                }
                else
                {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(_fileName, FileMode.Open))
                    {
                        docXML.Load(stream);
                    }
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(_fileName, FileMode.Truncate))
                    {
                        elRoot = docXML.DocumentElement;

                        XmlElement value = docXML.DocumentElement.SelectSingleNode(name) as XmlElement;
                        if (value == null)
                        {
                            value = docXML.CreateElement(name);
                            elRoot.AppendChild(value);
                        }
                        value.InnerText = stringValue;
                        stream.Seek(0, SeekOrigin.Begin);
                        docXML.Save(stream);
                    }
                }
            }
        }
    }
}
