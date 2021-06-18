using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PerksLib
{
    public class PerksCollection : List<Perk>,
        IPeristence,
        IXMLSerializable,
        ITxtSerializable,
        ICsvSerializable,
        JSONSerializable
    {
        public List<Perk> GetAllPerksWithField(Field field)
        {
            List<Perk> perks = new List<Perk>();
            foreach (var perk in this)
            {
                if (perk.ModifyField == field)
                {
                    perks.Add(perk);
                }
            }
            return perks;
        }

        public bool Load(string filename)
        {
            string extension = Path.GetExtension(filename);
            if (extension == ".xml")
            {
                return LoadXML(filename);
            }
            else if (extension == ".csv")
            {
                return LoadCSV(filename);
            }
            else if (extension == ".json")
            {
                return LoadJSON(filename);
            }
            else if (extension == ".txt")
            {
                return LoadTXT(filename);
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
            else if (extension == ".csv")
            {
                return SaveAsCSV(filename);
            }
            else if (extension == ".json")
            {
                return SaveAsJSON(filename);
            }
            else if (extension == ".txt")
            {
                return SaveAsTXT(filename);
            }
            return false;
        }
        
        public bool SaveAsXML(string filename)
        {
            if (Path.GetExtension(filename) != ".xml")
            {
                return false;
            }
            XmlSerializer xmlser = new XmlSerializer(typeof(PerksCollection));
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
        //
        public bool LoadXML(string filename)
        {
            this.Clear();
            if (!File.Exists(filename) || Path.GetExtension(filename) != ".xml")
            {
                return false;
            }
            XmlSerializer xmlser = new XmlSerializer(typeof(PerksCollection));
            try
            {

                using (StreamReader sr = new StreamReader(filename))
                {
                    this.Clear();
                    this.AddRange((PerksCollection)xmlser.Deserialize(sr));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        //
        public bool SaveAsJSON(string filename)
        {
            if (Path.GetExtension(filename) != ".json")
            {
                return false;
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write(JsonSerializer.Serialize(this, typeof(PerksCollection)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        //
        public bool LoadJSON(string filename)
        {
            this.Clear();
            if (!File.Exists(filename) || Path.GetExtension(filename) != ".json")
            {
                return false;
            }
            try
            {

                using (StreamReader sr = new StreamReader(filename))
                {
                    this.AddRange(JsonSerializer.Deserialize<PerksCollection>(sr.ReadToEnd()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        //
        public bool LoadCSV(string path)
        {
            this.Clear();
            if (!File.Exists(path) || Path.GetExtension(path) != ".csv")
            {
                return false;
            }

            using (StreamReader reader = new StreamReader(path))
            {
                string header = reader.ReadLine();
                while (reader.Peek() > 0)
                {
                    string line = reader.ReadLine();

                    Perk perk = new Perk();
                    if (Perk.TryParse(line, out perk))
                    {
                        this.Add(perk);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //
        public bool SaveAsCSV(string path)
        {
            if (Path.GetExtension(path) != ".csv")
            {
                return false;
            }
            FileStream fs;
            fs = File.Open(path, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("Name, Icon, ModifyField, Value, ModifyMethod, Cost, CustomDescription");
                foreach (var perk in this)
                {
                    writer.WriteLine(perk);
                }
            }
            return true;
        }

        public bool LoadTXT(string path)
        {
            this.Clear();
            if (!File.Exists(path) || Path.GetExtension(path) != ".txt")
            {
                return false;
            }

            using (StreamReader reader = new StreamReader(path))
            {
                string header = reader.ReadLine();
                while (reader.Peek() > 0)
                {
                    string line = reader.ReadLine();

                    Perk perk = new Perk();
                    if (Perk.TryParse(line, out perk))
                    {
                        this.Add(perk);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool SaveAsTXT(string path)
        {
            if (Path.GetExtension(path) != ".txt")
            {
                return false;
            }
            FileStream fs;
            fs = File.Open(path, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("Name, Icon, ModifyField, Value, ModifyMethod, Cost, CustomDescription");
                foreach (var perk in this)
                {
                    writer.WriteLine(perk);
                }
            }
            return true;
        }

        public List<Perk> FilterByName(string name)
        {
            List<Perk> perks = new List<Perk>();
            foreach (var perk in this)
            {
                if (perk.Name.StartsWith(name))
                {
                    perks.Add(perk);
                }
            }
            return perks;
        }
    }
}
