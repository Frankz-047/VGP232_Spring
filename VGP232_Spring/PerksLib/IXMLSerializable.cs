using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerksLib
{
    interface IXMLSerializable
    {
        bool SaveAsXML(string filename);
        bool LoadXML(string filename);
    }
}
