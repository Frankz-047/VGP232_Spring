using System;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Week_5_Console_Example
{
    class Program
    {
        public static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gZip = new GZipStream(memory,CompressionMode.Compress, true))
                {
                    gZip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }
        public static byte[] Decompress(byte[] compressed, int originalSize)
        {
            byte[] uncompressed = new byte[originalSize];
            using (MemoryStream memory = new MemoryStream(compressed))
            {
                using (GZipStream gZip = new GZipStream(memory, CompressionMode.Decompress, true))
                {
                    gZip.Read(uncompressed, 0, originalSize);
                }
                return uncompressed;
            }
        }

        public static void ZipArchiveExample()
        {
            string myDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string ImagesPath = Path.Combine(myDoc, "Images");
            string ImagesZip = Path.Combine(ImagesPath, "Image.zip");

            DirectoryInfo dir = new DirectoryInfo(ImagesPath);
            FileInfo[] allImagesFiles = dir.GetFiles("*.png");
            using (FileStream file = new FileStream(ImagesZip, FileMode.Create))
            {
                using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Update))
                {
                    foreach (var png in allImagesFiles)
                    {
                        ZipArchiveEntry entry = zip.CreateEntryFromFile(png.FullName, png.Name, CompressionLevel.Optimal);
                    }
                }
            }
        }

        public static void ZipArchiveExtractionExample()
        {
            string myDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string ImagesPath = Path.Combine(myDoc, "Images");
            string ImagesZip = Path.Combine(ImagesPath, "Image.zip");

            string extractedImagesPath = Path.Combine(myDoc, "Images/ExtractedImages");

            if (Directory.Exists(extractedImagesPath))
            {
                Directory.Delete(extractedImagesPath, true);
            }

            ZipFile.ExtractToDirectory(ImagesZip, extractedImagesPath);
        }
        static void Main(string[] args)
        {
            //byte[] text = Encoding.ASCII.GetBytes(new string('a',200000));
            //File.WriteAllBytes("raw.bin", text);

            //byte[] compressed = Compress(text);
            //File.WriteAllBytes("compressed.bin", compressed);

            //byte[] expand = Decompress(compressed, 200000);
            //File.WriteAllBytes("expand.bin", expand);

            ZipArchiveExtractionExample();
        }
    }
}
