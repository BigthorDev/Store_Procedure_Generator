using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using System.Reflection;


namespace SP_DAO_Generator.Utils
{
    public class ConfigUtil
    {
        public static string[] GetConnectionStringsStartingWith(string startWithName)
        {
            ConnectionStringSettingsCollection t = ConfigurationManager.ConnectionStrings;
            List<string> list = new List<string>();

            for (int i = 0; i < t.Count; i++)
            {
                if (StringUtil.ToString(t[i].Name).ToLower().StartsWith(startWithName))
                {
                    list.Add(t[i].Name);
                }
            }

            return list.ToArray();
        }


        public static string ReadSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static bool ReadBooleanSetting(string key)
        {
            string setting = ConfigurationManager.AppSettings[key];

            if (setting == null) //Default value when there is not key
                return true;
            else if (setting.ToLower() == "true")
                return true;
            else if (setting.ToLower() == "false")
                return false;

            //If not explicitly set to false, assume it is true
            return true;
        }

        public static string GetConnectionString(string key)
        {
            string retVal = "";

            ConnectionStringSettings connString = ConfigurationManager.ConnectionStrings[key];

            if (connString != null)
            {
                retVal = connString.ConnectionString;
            }

            return retVal;

        }

        public static string[] GetSettingsStartingWith(string startsWith)
        {
            List<string> ret = new List<string>();

            foreach (string s in ConfigurationManager.AppSettings)
            {
                if (StringUtil.ToString(s).ToLower().StartsWith(startsWith.ToLower()))
                {
                    ret.Add(s);
                }
            }

            return ret.ToArray();
        }

        public static void WriteSetting(string key, string value)
        {
            // load config document for current assembly
            XmlDocument doc = loadConfigDocument();

            // retrieve appSettings node
            XmlNode node = doc.SelectSingleNode("//appSettings");

            if (node == null)
                throw new InvalidOperationException("appSettings section not found in config file.");

            try
            {
                // select the 'add' element that contains the key
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

                if (elem != null)
                {
                    // add value for key
                    elem.SetAttribute("value", value);
                }
                else
                {
                    // key was not found so create the 'add' element 
                    // and set it's key/value attributes 
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", value);
                    node.AppendChild(elem);
                }
                doc.Save(getConfigFilePath());
            }
            catch
            {
                throw;
            }
        }

        public static void RemoveSetting(string key)
        {
            // load config document for current assembly
            XmlDocument doc = loadConfigDocument();

            // retrieve appSettings node
            XmlNode node = doc.SelectSingleNode("//appSettings");

            try
            {
                if (node == null)
                    throw new InvalidOperationException("appSettings section not found in config file.");
                else
                {
                    // remove 'add' element with coresponding key
                    node.RemoveChild(node.SelectSingleNode(string.Format("//add[@key='{0}']", key)));
                    doc.Save(getConfigFilePath());
                }
            }
            catch (NullReferenceException e)
            {
                throw new Exception(string.Format("The key {0} does not exist.", key), e);
            }
        }

        private static XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }

        private static string getConfigFilePath()
        {
            return Assembly.GetExecutingAssembly().Location + ".config";
        }



    }
}
