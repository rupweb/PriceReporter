using NUnit.Framework;
using PriceReader;
using System;
using System.IO;

namespace PriceReaderTests
{
    [TestFixture]
    public class PriceReaderTest
    {
        [TestCase("Data.txt")]
        public void FileRead(string name)
        {
            FileReader fr = new FileReader();
            string[] lines = fr.ReadFile(name);

            Assert.IsTrue(lines.Length > 0);
        }

        [TestCase("NotThere.txt")]
        public void FileNotRead(string name)
        {
            FileReader fr = new FileReader();
            string[] lines = new string[50];

            try
            {
                Assert.Throws<FileNotFoundException>(() => lines = fr.ReadFile(name));
            } 
            finally
            {
                Assert.IsNull(lines[0]);
            }          
        }
    }
}
