using System;
using System.IO;

namespace desafio_target_sistemas.Configurations
{
    public class Config
    {
        public static string GetConnection(string name)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[$"{name}"].ConnectionString;
        }
        public static string GetConnection()
        {
            return GetConnection("TargetBD");
        }


        public static string GetLogPath()
        {
            return GetLogPath("logPath");
        }
        public static string GetLogPath(string key)
        {
            string logPath = System.Configuration.ConfigurationManager.AppSettings[$"{key}"].ToString();
            logPath = Path.Combine(logPath, $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
            return logPath;
        }

        public static int GetCacheExpiration(string key)
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[key].ToString());
        }
    }
}