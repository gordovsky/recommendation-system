using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommendation_system
{
    //var reddit = new Reddit(@"C:\Users\col403\Desktop\reddit.csv");
    //reddit.ReadData();
    //reddit.RankNews();
    //reddit.Print();
    class Reddit
    {
        private DateTime _now;
        private string _path;
        private List<string[]> _data = new List<string[]>();
        private SortedDictionary<double, string> _news = new SortedDictionary<double, string>();
        public Reddit(string path)
        {
            _now = DateTime.Now;
            _path = path;
        }

        public void ReadData()
        {
            using (TextFieldParser reader = new TextFieldParser(_path))
            {
                reader.TextFieldType = FieldType.Delimited;
                reader.SetDelimiters(",");
                reader.HasFieldsEnclosedInQuotes = true;
                string[] currentLine;
                reader.ReadLine();
                while (!reader.EndOfData)
                {
                    currentLine = reader.ReadFields();
                    _data.Add(currentLine);
                }
            }
        }

        public double GetRate(string[] post)
        {
            double Ts = (_now - DateTime.Parse(post[1])).TotalSeconds;
            Int64 x = Int64.Parse(post[2]) - Int64.Parse(post[3]);
            int y = 0;
            if (x > 0)
                y = 1;
            if (x == 0)
                y = 0;
            if (x < 0)
                y = -1;
            Int64 z = (Math.Abs(x) >= 0) ? Math.Abs(x) : 1;
            return Math.Log10(z) + y * Ts / 45000;
        }

        public void RankNews()
        {
            foreach (var row in _data)
            {
                _news.Add(GetRate(row), row[0]);
            }
        }

        public void Print()
        {
            foreach (var el in _news)
            {
                Console.WriteLine("{0} Rate is : {1}", el.Value, el.Key);
            }
        }
    }
}
