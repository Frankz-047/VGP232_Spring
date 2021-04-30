using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3
{
    interface IPersistence
    {
        bool Save(string filename);
        bool Load(string filename);
    }
}
