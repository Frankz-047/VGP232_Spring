using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3
{
    interface BinarySerializable
    {
        bool SaveAsBinary(string filename);
        bool LoadBinary(string filename);
    }
}
