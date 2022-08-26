using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace read_write_files.Models
{
    public class Base
    {
        // path = database/products.csv
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFolderFile(string path)
        {
            string folder = path.Split("/")[0];

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public List<string> ReadAllLinesCSV(string path)
        {
            List<string> lines = new();

            using (StreamReader file = new(path))
            {
                string line;

                while ( (line = file.ReadLine()) != null )
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        public void RewriteCSV(string path, List<string> lines)
        {
            using (StreamWriter output = new(path))
            {
                foreach (var line in lines)
                {
                    output.WriteLine(line);
                }
            }
        }
    }
}
