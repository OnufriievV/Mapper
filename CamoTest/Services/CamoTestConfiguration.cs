using System;
using System.Configuration;

namespace CamoTest.Services
{
    public static class CamoTestConfiguration
    {
        public static string TemporaryFileExtension => ConfigurationHelper.GetSetting("TemporaryFileExtension");

        public static int TemporaryFileStorageTime => Convert.ToInt32(ConfigurationHelper.GetSetting("TemporaryFileStorageTime")); 


    }

    static class ConfigurationHelper
    {
        public static string GetSetting(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (name.Length == 0)
                throw new ArgumentException($"Error argument \"{nameof(name)}\" is an empty string");

            string result = null;

            // Check in configuration file
            result = ConfigurationManager.AppSettings[name];
            if (result != null)
                return result;

            return result;
        }
    }

}
