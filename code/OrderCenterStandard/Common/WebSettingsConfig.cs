using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.CompilerServices;
using System.Configuration;

namespace OrderCenterStandard.Common
{
    public class WebSettingsConfig
    {
        public static string UrlExpireTime
        {
            get
            {
                return AppSettingValue();
            }
        }

        private static string AppSettingValue([CallerMemberName] string key = null)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}