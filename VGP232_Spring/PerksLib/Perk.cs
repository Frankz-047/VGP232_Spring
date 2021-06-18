using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Field
{
    None,
    Health,
    Ammo,
    ReloadTime,
    MoveSpeed,
    MagSize,
    FireRate
}

public enum Method
{
    None,
    Add,
    Mutiply
}


namespace PerksLib
{
    public class Perk
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public Field ModifyField { get; set; }
        public float Value { get; set; }
        public Method ModifyMethod { get; set; }
        public int Cost { get; set; }
        public string CustomDescription { get; set; }

        public static bool TryParse(string rawData, out Perk perk)
        {
            string[] values = rawData.Split(',');
            perk = new Perk();
            if (values.Length == 7)
            {
                try
                {
                    perk.Name = values[0];
                    perk.Icon = values[1];
                    perk.ModifyField = Enum.Parse<Field>(values[2]);
                    perk.Value = float.Parse(values[3]);
                    perk.ModifyMethod = Enum.Parse<Method>(values[4]);
                    perk.Cost = int.Parse(values[5]);
                    perk.CustomDescription = values[6];
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format($"{Name},{Icon},{ModifyField},{Value},{ModifyMethod},{Cost},{CustomDescription}");
        }
    }
}

