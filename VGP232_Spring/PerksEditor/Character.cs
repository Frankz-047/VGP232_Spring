using PerksLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerksEditor
{
    public class Character
    {
        public float Health { get; set; }
        public float Ammo { get; set; }
        public float ReloadTime { get; set; }
        public float MoveSpeed { get; set; }
        public float MagSize { get; set; }
        public float FireRate { get; set; }

        public Character()
        {
            Health = 100;
            Ammo = 30;
            ReloadTime = 1;
            MoveSpeed = 5;
            MagSize = 6;
            FireRate = 0.3f;
        }

        public void ProcessPerk(Perk perk)
        {
            switch (perk.ModifyField)
            {
                case Field.Health:
                    Health = ApplyChange(perk, Health);
                    break;
                case Field.Ammo:
                    Ammo = ApplyChange(perk, Ammo);
                    break;
                case Field.ReloadTime:
                    ReloadTime = ApplyChange(perk, ReloadTime);
                    break;
                case Field.MoveSpeed:
                    MoveSpeed = ApplyChange(perk, MoveSpeed);
                    break;
                case Field.MagSize:
                    MagSize = ApplyChange(perk, MagSize);
                    break;
                case Field.FireRate:
                    FireRate = ApplyChange(perk, FireRate);
                    break;
                default:
                    break;
            }
        }

        public float ApplyChange(Perk perk, float original)
        {
            float modifiedValue = original;
            if (perk.ModifyMethod == Method.Add)
            {
                modifiedValue += perk.Value;
            }
            else if (perk.ModifyMethod == Method.Mutiply)
            {
                if (perk.Value > 0)
                {
                    modifiedValue *= (perk.Value * 0.01f);
                }
                else if (perk.Value < 0)
                {
                    modifiedValue /= (perk.Value * -0.01f);
                }
                else if (perk.Value == 0)
                {
                    return modifiedValue;
                }
            }
            return modifiedValue;
        }
    }
}
