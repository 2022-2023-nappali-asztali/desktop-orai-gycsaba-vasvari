using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KretaDesktop.Localization
{
    public class ProjectLocalization
    {
        public void SwitchToCurrentCuture()
        {
            var languageDictionary = new ResourceDictionary();

            string currentCultureName = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            string url = GetLocXAMLFilePath(currentCultureName);
            
            languageDictionary.Source = new Uri(url, UriKind.Relative);

            int index = GetLanguageDictionaryIndex();
            if (index == -1)
            {
                // Add in newly loaded Resource Dictionary
                Application.Current.Resources.MergedDictionaries.Add(languageDictionary);
            }
            else
            {
                // Replace the current langage dictionary with the new one
                Application.Current.Resources.MergedDictionaries[index] = languageDictionary;
            }
        }

        private string GetLocXAMLFilePath(string cultureName)
        {
            StringBuilder path = new StringBuilder();
            path.Append("..\\Localization\\Resources\\" + cultureName + "\\StringResources.xaml");
            return path.ToString();
        }

        private int GetLanguageDictionaryIndex()
        {
            int langDictId = -1;
            bool found = false;
            for (int i = 0; i < Application.Current.Resources.MergedDictionaries.Count && !found; i++)
            {
                var md = Application.Current.Resources.MergedDictionaries[i].Source.ToString();
                if (md.Contains("ResourceDictionaryName"))
                {
                    langDictId = i;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return -1;
            }
            else
            {
                return langDictId;
            }
        }

        public string GetStringResource(string stringResourceName)
        {

            string url = GetLocXAMLFilePath(CultureInfo.CurrentCulture.Name);


            string result = string.Empty;
            ResourceDictionary res = Application.LoadComponent(new Uri(url, UriKind.Relative)) as ResourceDictionary;
            if ((res != null) && (res.Contains(stringResourceName)))
                result = res[stringResourceName].ToString();
            return result;

        }
    }
}
