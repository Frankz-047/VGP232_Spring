using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Week3
{
    [Serializable]
    public class Skill
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Modifier { get; set; }
        public int ReCharge { get; set; }
        public override string ToString()
        {
            return string.Format($"Name: {Name}, Cost: {Cost}, Modifier: {Modifier}, ReCharge: {ReCharge}");
        }
    }
}
