using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponLib
{
    interface ICsvSerializable
    {
        bool LoadCSV(string path);
        bool SaveAsCSV(string path);
    }
}
