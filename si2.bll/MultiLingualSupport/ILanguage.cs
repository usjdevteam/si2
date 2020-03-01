using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.MultiLingualSupport
{
    public interface ILanguage
    {
        string Code { get; }
        string Name { get; }
    }
}
