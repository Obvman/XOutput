using System.Collections.Generic;
using XOutput.UI;

namespace XOutput
{
    /// <summary>
    /// Contains the language management.
    /// </summary>
    public sealed class LanguageModel : ModelBase
    {
        private Dictionary<string, string> _data;

        public static LanguageModel Instance { get; } = new LanguageModel();

        public Dictionary<string, string> Data { get => _data; set => SetProperty(ref _data, value); }

        public string Translate(string key) => Translate(_data, key);

        public static string Translate(Dictionary<string, string> translation, string key)
        {
            if (translation == null || key == null || !translation.ContainsKey(key))
            {
                return key;
            }
            return translation[key];
        }
    }
}
