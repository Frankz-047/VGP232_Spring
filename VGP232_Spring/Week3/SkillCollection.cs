using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace Week3
{
    public class SkillCollection 
        : List<Skill>
        , IPersistence
        , IXMLSerializable
        , BinarySerializable
        , JSONSerializable
    {
        public bool Load(string filename)
        {
            string extension = Path.GetExtension(filename);
            if (extension == ".xml")
            {
                return LoadXML(filename);
            }
            else if (extension == ".bin")
            {
                return LoadBinary(filename);
            }
            else if (extension == ".json")
            {
                return LoadJSON(filename);
            }
            return false;
        }

        public bool Save(string filename)
        {
            string extension = Path.GetExtension(filename);
            if (extension == ".xml")
            {
                return SaveAsXML(filename);
            }
            else if (extension == ".bin")
            {
                return SaveAsBinary(filename);
            }
            else if (extension == ".json")
            {
                return SaveAsJSON(filename);
            }
            return false;
        }

        public bool LoadBinary(string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    this.Clear();
                    this.AddRange((SkillCollection)bf.Deserialize(sr.BaseStream));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool SaveAsBinary(string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    bf.Serialize(sw.BaseStream, this);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            //fs.Close();
            return true;
        }

        public bool LoadJSON(string filename)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    this.AddRange(JsonSerializer.Deserialize<SkillCollection>(sr.ReadToEnd()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool SaveAsJSON(string filename)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write(JsonSerializer.Serialize<SkillCollection>(this));
                    //sw.Write(JsonSerializer.Serialize(this, typeof(SkillCollection)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool LoadXML(string filename)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(SkillCollection));
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    this.Clear();
                    this.AddRange((SkillCollection)xmlser.Deserialize(sr));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool SaveAsXML(string filename)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(SkillCollection));

            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    xmlser.Serialize(sw, this);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
