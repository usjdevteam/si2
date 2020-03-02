using System.Collections.Generic;
using System.Text.Json;

namespace si2.bll.MultiLingualSupport
{
    public class MLString
    {
        private Dictionary<string, string> _values;
        public MLString(ILanguage l, string s)
        {
            _values = new Dictionary<string, string>();
            _values[l.Code] = s;
        }

        public MLString(string s)
        {
            _values=JsonSerializer.Deserialize<Dictionary<string, string>>(s);
        }

        public string Value(ILanguage l)
        {
            if (_values.ContainsKey(l.Code))
            {
                return _values[l.Code];
            }
            throw new UndefinedValueForLanguageException("Undefined value for language " + l.Name);
        }

        public void Set(ILanguage language, string Value)
        {
            _values[language.Code] = Value;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(_values);
        }
    }
}
