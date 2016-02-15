using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMaterialCalculator
{
    /// <summary>
    ///UI语言设置模块类
    /// </summary>
    public class UILanguageSet
    {
        public void ReadConfigAndSetUILanguage()
        {
            string languageType = Properties.Settings.Default.Language;
            if (string.IsNullOrEmpty(languageType))
            {
                return;
            }

            string languagePath = "Language/" + languageType + ".xaml";
            Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() {
                Source = new Uri(languagePath,UriKind.Relative )};
        }



    }
}
