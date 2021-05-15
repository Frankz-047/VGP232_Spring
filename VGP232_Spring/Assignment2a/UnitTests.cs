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

        const string OUTPUT_FILE_CSV = "weapons.csv";
        const string OUTPUT_FILE_XML = "weapons.xml";
        const string OUTPUT_FILE_JSON = "weapons.json";

        const string EMPTY_FILE_CSV = "empty.csv";
        const string EMPTY_FILE_XML = "empty.xml";
        const string EMPTY_FILE_JSON = "empty.json";

        // A helper function to get the directory of where the actual path is.
        private string CombineToAppPath(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
        }

        [SetUp]
        public void SetUp()
        {
            inputPath = CombineToAppPath(INPUT_FILE);
            outputPath = CombineToAppPath(OUTPUT_FILE_CSV);
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

        // Serialization Test
        //Valid JSON
        [TestCase(95)]
        public void WeaponCollection_Load_Save_Load_ValidJson(int expectedValue)
        {
            outputPath = CombineToAppPath(OUTPUT_FILE_JSON);
            WeaponCollection.Save(outputPath);
            Assert.IsTrue(WeaponCollection.Load(outputPath));
            Assert.AreEqual(expectedValue, WeaponCollection.Count());
        }
        [TestCase(95)]
        public void WeaponCollection_Load_SaveAsJSON_Load_ValidJson(int expectedValue)
        {
            outputPath = CombineToAppPath(OUTPUT_FILE_JSON);
            WeaponCollection.SaveAsJSON(outputPath);
            Assert.IsTrue(WeaponCollection.Load(outputPath));
            Assert.AreEqual(expectedValue, WeaponCollection.Count());
        }
        [TestCase(95)]
        public void WeaponCollection_Load_SaveAsJSON_LoadJSON_ValidJson(int expectedValue)
        {
            outputPath = CombineToAppPath(OUTPUT_FILE_JSON);
            WeaponCollection.SaveAsJSON(outputPath);
            Assert.IsTrue(WeaponCollection.LoadJSON(outputPath));
            Assert.AreEqual(expectedValue, WeaponCollection.Count());
        }
        [TestCase(95)]
        public void WeaponCollection_Load_Save_LoadJSON_ValidJson(int expectedValue)
        {
            outputPath = CombineToAppPath(OUTPUT_FILE_JSON);
            WeaponCollection.Save(outputPath);
            Assert.IsTrue(WeaponCollection.LoadJSON(outputPath));
            Assert.AreEqual(expectedValue, WeaponCollection.Count());
        }

        //Valid CSV
        [TestCase(95)]
        public void WeaponCollection_Load_Save_Load_ValidCsv(int expectedValue)
        {
            outputPath = CombineToAppPath(OUTPUT_FILE_CSV);
            WeaponCollection.Save(outputPath);
            Assert.IsTrue(WeaponCollection.Load(outputPath));
            Assert.AreEqual(expectedValue, WeaponCollection.Count());
        }
        [TestCase(95)]
        public void WeaponCollection_Load_SaveAsCSV_LoadCSV_ValidCsv(int expectedValue)
        {
            outputPath = CombineToAppPath(OUTPUT_FILE_CSV);
            WeaponCollection.SaveAsCSV(outputPath);
            Assert.IsTrue(WeaponCollection.LoadCSV(outputPath));
            Assert.AreEqual(expectedValue, WeaponCollection.Count());
        }

        //Valid XML
        [TestCase(95)]
        public void WeaponCollection_Load_Save_Load_ValidXml(int expectedValue)
        {
            outputPath = CombineToAppPath(OUTPUT_FILE_XML);
            WeaponCollection.Save(outputPath);
            Assert.IsTrue(WeaponCollection.Load(outputPath));
            Assert.AreEqual(expectedValue, WeaponCollection.Count());
        }
        [TestCase(95)]
        public void WeaponCollection_Load_SaveAsXML_LoadXML_ValidXml(int expectedValue)
        {
            outputPath = CombineToAppPath(OUTPUT_FILE_XML);
            WeaponCollection.SaveAsXML(outputPath);
            Assert.IsTrue(WeaponCollection.LoadXML(outputPath));
            Assert.AreEqual(expectedValue, WeaponCollection.Count());
        }

        //Empty JSON
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidJson()
        {
            outputPath = CombineToAppPath(EMPTY_FILE_JSON);
            WeaponCollection empty = new WeaponCollection();
            empty.Clear();
            empty.SaveAsJSON(outputPath);
            empty.Load(outputPath);
            Assert.IsFalse(empty.Any());
        }

        //Empty CSV
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidCsv()
        {
            outputPath = CombineToAppPath(EMPTY_FILE_CSV);
            WeaponCollection empty = new WeaponCollection();
            empty.Clear();
            empty.SaveAsCSV(outputPath);
            empty.Load(outputPath);
            Assert.IsFalse(empty.Any());
        }

        //Empty XML
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidXml()
        {
            outputPath = CombineToAppPath(EMPTY_FILE_XML);
            WeaponCollection empty = new WeaponCollection();
            empty.Clear();
            empty.SaveAsXML(outputPath);
            empty.Load(outputPath);
            Assert.IsFalse(empty.Any());
        }

        //Test Load InvalidFormat
        [Test]
        public void WeaponCollection_Load_SaveJSON_LoadXML_InvalidXml()
        {
            outputPath = CombineToAppPath(OUTPUT_FILE_JSON);
            WeaponCollection.SaveAsJSON(outputPath);
            Assert.IsFalse(WeaponCollection.LoadXML(outputPath));
            Assert.IsFalse(WeaponCollection.Any());
        }

        [Test]
        public void WeaponCollection_Load_SaveXML_LoadJSON_InvalidJson()
        {
            outputPath = CombineToAppPath(OUTPUT_FILE_XML);
            WeaponCollection.SaveAsXML(outputPath);
            Assert.IsFalse(WeaponCollection.LoadJSON(outputPath));
            Assert.IsFalse(WeaponCollection.Any());
        }

        [Test]
        public void WeaponCollection_ValidCsv_LoadXML_InvalidXml()
        {
            Assert.IsFalse(WeaponCollection.LoadXML(inputPath));
            Assert.IsFalse(WeaponCollection.Any());
        }

        [Test]
        public void WeaponCollection_ValidCsv_LoadJSON_InvalidJson()
        {
            Assert.IsFalse(WeaponCollection.LoadJSON(inputPath));
            Assert.IsFalse(WeaponCollection.Any());
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
        //    Assert.IsFalse(weaponCollection.Load("afilethatdoesnotexists.csv"));
        //    Assert.AreEqual(weaponCollection.Count, 0);
        //}
        [Test]
        public void WeaponCollection_LoadThatDoesNotExist_FalseAndEmpty()
        {
            // TODO: load returns false, expect an empty WeaponCollection
            WeaponCollection.Clear();
            Assert.IsFalse(WeaponCollection.Load("Nothing"));
            Assert.AreEqual(0 , WeaponCollection.Count);
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
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.BaseAttack, actual.BaseAttack);
            Assert.AreEqual(expected.Image, actual.Image);
            Assert.AreEqual(expected.Rarity, actual.Rarity);
            Assert.AreEqual(expected.SecondaryStat, actual.SecondaryStat);
            Assert.AreEqual(expected.Passive, actual.Passive);
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
