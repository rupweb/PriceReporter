using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceReader
{
    // This class reads the data file

    public class FileReader
    {
        public String[] ReadFile(String filename)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            return File.ReadAllLines(path);
        }
    }
}
