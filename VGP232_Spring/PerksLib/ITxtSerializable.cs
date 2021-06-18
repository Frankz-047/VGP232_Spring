using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerksLib
{
    interface ITxtSerializable
    {
        bool LoadTXT(string path);
        bool SaveAsTXT(string path);
    }
}
