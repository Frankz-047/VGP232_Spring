using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeaponLib
{
    public class WeaponCollection : List<Weapon>,
        IPeristence,
        IXMLSerializable,
        JSONSerializable,
        ICsvSerializable
    {
        public int GetHighestBaseAttack()
        {
            int highestBaseAttack = 0;
            foreach (var weapon in this)
            {
                if (weapon.BaseAttack >= highestBaseAttack)
                {
                    highestBaseAttack = weapon.BaseAttack;
                }
            }
            return highestBaseAttack;
        }
        public int GetLowestBaseAttack()
        {
            int lowestBaseAttack = int.MaxValue;
            foreach (var weapon in this)
            {
                if (weapon.BaseAttack <= lowestBaseAttack)
                {
                    lowestBaseAttack = weapon.BaseAttack;
                }
            }
            return lowestBaseAttack;
        }
        public List<Weapon> GetAllWeaponsOfType(WeaponType type)
        {
            List<Weapon> weapons = new List<Weapon>();
            foreach (var weapon in this)
            {
                if (weapon.Type == type)
                {
                    weapons.Add(weapon);
                }
            }
            return weapons;
        }
        public List<Weapon> GetAllWeaponsOfRarity(int stars)
        {
            List<Weapon> weapons = new List<Weapon>();
            foreach (var weapon in this)
            {
                if (weapon.Rarity == stars)
                {
                    weapons.Add(weapon);
                }
            }
            return weapons;
        }

        public List<Weapon> FilterByName(string name)
        {
            {
                List<Weapon> weapons = new List<Weapon>();
                foreach (var weapon in this)
                {
                    if (weapon.Name.StartsWith(name))
                    {
                        weapons.Add(weapon);
                    }
                }
                return weapons;
            }
        }

        //ERROR: -5. You are comparing against an specific name. Get the columnName first and make it
        //all lower case before you compare with lower case names.
        //switch(columnName.ToLower()){
        // case "name":
        // ...

        //Also, where are the new properties like image, secondarystat, passive?
        public void SortBy(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "name":
                    this.Sort(Weapon.CompareByName);
                    break;
                case "type":
                    this.Sort(Weapon.CompareByType);
                    break;
                case "rarity":
                    this.Sort(Weapon.CompareByRarity);
                    break;
                case "image":
                    this.Sort(Weapon.CompareByImage);
                    break;
                case "baseattack":
                    this.Sort(Weapon.CompareByBaseAttack);
                    break;
                case "passive":
                    this.Sort(Weapon.CompareByPassive);
                    break;
                case "secondarystat":
                    this.Sort(Weapon.CompareBySecondaryStat);
                    break;
                default:
                    break;
            }
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
            return false;
        }

        //ERROR: -1.Missing Elements in header:
        //Header is: "Name, Type, Image, Rarity, BaseAttack, SecondaryStat, Passive"
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
            return false;
        }

        public bool SaveAsXML(string filename)
        {
            if (Path.GetExtension(filename) != ".xml")
            {
                return false;
            }
            XmlSerializer xmlser = new XmlSerializer(typeof(WeaponCollection));
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

        public bool LoadXML(string filename)
        {
            this.Clear();
            if (!File.Exists(filename) || Path.GetExtension(filename) != ".xml")
            {
                return false;
            }
            XmlSerializer xmlser = new XmlSerializer(typeof(WeaponCollection));
            try
            {

                using (StreamReader sr = new StreamReader(filename))
                {
                    this.Clear();
                    this.AddRange((WeaponCollection)xmlser.Deserialize(sr));
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
            if (Path.GetExtension(filename) != ".json")
            {
                return false;
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write(JsonSerializer.Serialize(this, typeof(WeaponCollection)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

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
                    this.AddRange(JsonSerializer.Deserialize<WeaponCollection>(sr.ReadToEnd()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

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

                    Weapon weapon = new Weapon();
                    if (Weapon.TryParse(line, out weapon))
                    {
                        this.Add(weapon);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

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
                writer.WriteLine("Name, Type, Image, Rarity, BaseAttack, SecondaryStat, Passive");
                foreach (var weapon in this)
                {
                    writer.WriteLine(weapon);
                }
            }
            return true;
        }
    }
}
