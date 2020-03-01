using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.MultiLingualSupport
{
    class UnsupportedLanguageException: Exception
    {
        public UnsupportedLanguageException(string lang):base("Unsupported language " + lang)
        {

        }
    }
}
