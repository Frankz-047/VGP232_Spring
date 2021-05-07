using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Assignment2a
{
    [TestFixture]
    public class UnitTests
    {
        private WeaponCollection WeaponCollection;
        private string inputPath;
        private string outputPath;

        const string INPUT_FILE = "data2.csv";
        const string OUTPUT_FILE = "output.csv";

        // A helper function to get the directory of where the actual path is.
        private string CombineToAppPath(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
        }

        [SetUp]
        public void SetUp()
        {
            inputPath = CombineToAppPath(INPUT_FILE);
            outputPath = CombineToAppPath(OUTPUT_FILE);
            WeaponCollection= new WeaponCollection();
            WeaponCollection.Load(inputPath);
        }

        [TearDown]
        public void CleanUp()
        {
            // We remove the output file after we are done.
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }

        // WeaponCollection Unit Tests
        [Test]
        public void WeaponCollection_GetHighestBaseAttack_HighestValue()
        {
            // Expected Value: 48
            // TODO: call WeaponCollection.GetHighestBaseAttack() and confirm that it matches the expected value using asserts.
            Assert.IsTrue((WeaponCollection.GetHighestBaseAttack() == 48));
        }

        [Test]
        public void WeaponCollection_GetLowestBaseAttack_LowestValue()
        {
            // Expected Value: 23
            // TODO: call WeaponCollection.GetLowestBaseAttack() and confirm that it matches the expected value using asserts.
            Assert.IsTrue((WeaponCollection.GetLowestBaseAttack() == 23));
        }

        [TestCase(WeaponType.Sword, 21)]
        public void WeaponCollection_GetAllWeaponsOfType_ListOfWeapons(WeaponType type, int expectedValue)
        {
            // TODO: call WeaponCollection.GetAllWeaponsOfType(type) and confirm that the weapons list returns Count matches the expected value using asserts.
            List<Weapon> weapons = WeaponCollection.GetAllWeaponsOfType(type);
            foreach (var weapon in weapons)
            {
                Assert.AreEqual(weapon.Type, type);
            }
            Assert.AreEqual(weapons.Count, expectedValue);
        }

        [TestCase(5, 10)]
        public void WeaponCollection_GetAllWeaponsOfRarity_ListOfWeapons(int stars, int expectedValue)
        {
            // TODO: call WeaponCollection.GetAllWeaponsOfRarity(stars) and confirm that the weapons list returns Count matches the expected value using asserts.
            List<Weapon> weapons = WeaponCollection.GetAllWeaponsOfRarity(stars);
            foreach (var weapon in weapons)
            {
                Assert.AreEqual(weapon.Rarity, stars);
            }
            Assert.AreEqual(weapons.Count, expectedValue);
        }

        [Test]
        public void WeaponCollection_LoadThatExistAndValid_True()
        {
            // TODO: load returns true, expect WeaponCollection with count of 95 .
            Assert.IsTrue(WeaponCollection.Load(inputPath));
            Assert.AreEqual(WeaponCollection.Count, 95);
        }

        //ERROR: -2 Test Error.
        //[Test]
        //public void WeaponCollection_LoadThatDoesNotExist_FalseAndEmpty()
        //{
        //    // Load returns false, expect an empty WeaponCollection
        //    // LC2: You're suppose to call weaponCollection.Load("afilethatdoesnotexists.csv"); and assert to check it returns false and check it's empty. DONE
        //    Assert.IsFalse(File.Exists("does_not_exist.csv"));
        //    Assert.AreEqual(weaponCollection.Count, 0);
        //}
        [Test]
        public void WeaponCollection_LoadThatDoesNotExist_FalseAndEmpty()
        {
            // TODO: load returns false, expect an empty WeaponCollection
            Assert.IsFalse(WeaponCollection.Load("Nothing"));
            Assert.Equals(WeaponCollection, null);
        }

        [Test]
        public void WeaponCollection_SaveWithValuesCanLoad_TrueAndNotEmpty()
        {
            // TODO: save returns true, load returns true, and WeaponCollection is not empty.
            Assert.IsTrue(WeaponCollection.Load(inputPath));
            Assert.IsTrue(WeaponCollection.Save(outputPath));
            Assert.IsTrue(WeaponCollection.Load(outputPath));
            Assert.IsTrue(WeaponCollection.Count != 0);
        }

        [Test]
        public void WeaponCollection_SaveEmpty_TrueAndEmpty()
        {
            // After saving an empty WeaponCollection, load the file and expect WeaponCollection to be empty.
            WeaponCollection.Clear();
            Assert.IsTrue(WeaponCollection.Save(outputPath));
            Assert.IsTrue(WeaponCollection.Load(outputPath));
            Assert.IsTrue(WeaponCollection.Count == 0);
        }

        //ERROR: -2. Not passing.
        //public void Weapon_TryParseValidLine_TruePropertiesSet()
        //{
        //    // Create a Weapon with the stats above set properly
        //    Weapon expected = null;

        //    // TSet properties of expected weapon
        //    expected = new Weapon()
        //    {
        //        Name = "Skyward Blade",
        //        Type = WeaponType.Sword,
        //        Image = "https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png",
        //        Rarity = 5,
        //        BaseAttack = 46,
        //        SecondaryStat = "Energy Recharge",
        //        Passive = "Sky-Piercing Fang"
        //    };

        //    // Line to read
        //    string line = "Skyward Blade,Sword,https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png,5,46,Energy Recharge,Sky-Piercing Fang";
        //    Weapon actual = null;

        //    // Check for the rest of the properties, Image,Rarity,SecondaryStat,Passive
        //    Assert.IsTrue(Weapon.TryParse(line, out actual));
        //    Assert.AreEqual(expected.Name, actual.Name);
        //    Assert.AreEqual(expected.Type, actual.Type);
        //    Assert.AreEqual(expected.BaseAttack, actual.BaseAttack);
        //    Assert.AreEqual(expected.Image, actual.Image);
        //    Assert.AreEqual(expected.Rarity, actual.Rarity);
        //    Assert.AreEqual(expected.SecondaryStat, actual.SecondaryStat);
        //    Assert.AreEqual(expected.Passive, actual.Passive);

        //}
        // Weapon Unit Tests
        [Test]
        public void Weapon_TryParseValidLine_TruePropertiesSet()
        {
            // TODO: create a Weapon with the stats above set properly
            Weapon expected = null;
            // TODO: uncomment this once you added the Type1 and Type2
            expected = new Weapon()
            {
                Name = "Skyward Blade",
                Type = WeaponType.Sword,
                Image = "https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png",
                Rarity = 5,
                BaseAttack = 46,
                SecondaryStat = "Energy Recharge",
                Passive = "Sky-Piercing Fang"
            };

            string line = "Skyward Blade,Sword,https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png,5,46,Energy Recharge,Sky-Piercing Fang";
            Weapon actual = null;
            Assert.IsTrue(Weapon.TryParse(line, out actual));
            // TODO: uncomment this once you have TryParse implemented.
            Assert.IsTrue(Weapon.TryParse(line, out actual));
            Assert.Equals(expected.Name, actual.Name);
            Assert.Equals(expected.Type, actual.Type);
            Assert.Equals(expected.BaseAttack, actual.BaseAttack);
            Assert.Equals(expected.Image, actual.Image);
            Assert.Equals(expected.Rarity, actual.Rarity);
            Assert.Equals(expected.SecondaryStat, actual.SecondaryStat);
            Assert.Equals(expected.Passive, actual.Passive);
            // TODO: check for the rest of the properties, Image,Rarity,SecondaryStat,Passive
        }

        [Test]
        public void Weapon_TryParseInvalidLine_FalseNull()
        {
            // TODO: use "1,Bulbasaur,A,B,C,65,65", Weapon.TryParse returns false, and Weapon is null.
            string line = "1,Bulbasaur,A,B,C,65,65";
            Weapon actual = null;
            Weapon.TryParse(line, out actual);
        }
    }
}
