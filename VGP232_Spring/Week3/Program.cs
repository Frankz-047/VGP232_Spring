using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Week3
{
    class Program
    {
        static void Main(string[] args)
        {
            Skill MySkill = new Skill() { Name = "Fire 1", Cost = 800, Modifier = 100 };
            //SaveSkill(MySkill);

            //LoadBit("MySkill.dat");
            string saveFile = "skillBool.json";
            SkillCollection sc = new SkillCollection()
            {
                new Skill(){Name = "Fire I", Cost = 5, Modifier = 5 , ReCharge = 3},
                new Skill(){Name = "Bllizard I", Cost = 10, Modifier = 12 , ReCharge = 2},
                new Skill(){Name = "Fire IV", Cost = 160, Modifier = 15 , ReCharge = 6},
                new Skill(){Name = "Flare", Cost = 320, Modifier = 8 , ReCharge = 8},
                new Skill(){Name = "Sleep", Cost = 1, Modifier = 1 , ReCharge = 10},
            };
            sc.Save(saveFile);

            var sc_1 = new SkillCollection();
            sc_1.Load(saveFile);
            foreach (var skill in sc_1)
            {
                Console.WriteLine(skill);
            }
        }

        private static void SaveSkill(Skill skill)
        {
            //op file
            FileStream fs = new FileStream("MySkill.dat", FileMode.Create);
            //binary formatter
            BinaryWriter bw = new BinaryWriter(fs);
            BinaryFormatter bf = new BinaryFormatter();
            //serialize file
            bf.Serialize(fs, skill);
            Console.WriteLine("Binary file Saved!");
            fs.Close();
        }

        private static void LoadBit(string filename)
        {
            FileStream fs = new FileStream("MySkill.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Skill skill1 = (Skill)bf.Deserialize(fs);
            Console.WriteLine("Binary file Loaded!");
            Console.WriteLine(skill1);
            fs.Close();
        }
    }
}
