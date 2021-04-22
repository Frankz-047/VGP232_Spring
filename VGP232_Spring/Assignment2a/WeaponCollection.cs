using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2a
{
    class WeaponCollection : List<Weapon>, IPeristence
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
        public void SortBy(string columnName)
        {
            if (columnName == "Name")
            {
                this.Sort(Weapon.CompareByName);
            }
            else if (columnName == "Type")
            {
                this.Sort(Weapon.CompareByType);
            }
            else if (columnName == "Rarity")
            {
                this.Sort(Weapon.CompareByRarity);
            }
            else if (columnName == "BaseAttack")
            {
                this.Sort(Weapon.CompareByBaseAttack);
            }
        }
        public bool Load(string filename)
        {
            this.Clear();
            if (!File.Exists(filename))
            {
                return false;
            }
            using (StreamReader reader = new StreamReader(filename))
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

        public bool Save(string filename)
        {
            FileStream fs;
            fs = File.Open(filename, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("Name,Type,Rarity,BaseAttack");
                foreach (var weapon in this)
                {
                    writer.WriteLine(weapon);
                }
            }
            return true;    
        }
    }
}
