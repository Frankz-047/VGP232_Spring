using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerksLib
{
    interface JSONSerializable
    {
        bool SaveAsJSON(string filename);
        bool LoadJSON(string filename);
    }
}
