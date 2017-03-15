using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommendation_system
{
    //Naive Bayes classifier
    class NBC
    {
        private string[] _keywords;
        private string[] _movies;
        private bool[,] _matrix;
        private bool[] _labels;

        public NBC()
        {
            _movies = new string[] { "The Lobster", "Control", "Taxi Driver", "Into the Wild", "Dead Man", "Apocalypse Now", "The Wall",
                                     "The Fate of the Furious", "Logan", "Shrek", "The Amazing Spider-Man 2", "R.I.P.D.", "The Smurfs", "Prince of Persia: The Sands of Time",
                                     "X"
                                    };
            _labels = new bool[] { true, true, true, true, true, true, true,
                                   false, false, false, false, false, false, false };
            _keywords = new string[] { "3d", "drama", "surreal", "cult", "action", "classic", "atmospheric", "adventure", "fantasy", "epic", "magic", "dwarf", "elf", "music" };
            _matrix = new bool[,] {
                                    { false, true , true, false, false, false, true, true, false, false, false, false, false, false },
                                    { false, true, true, true, false, true, true, false, false, false, false, false, false, true},
                                    { false, true, false, true, true, true, true, false, false, false, false, false, false, false},
                                    { false, true, false, true, true, false, true, true, false, false, false, false, false, false},
                                    { false, true, true, true, false, true, true, true, false, false, false, false, false, true},
                                    { false, true, true, true, false, true, true, true, false, false, false, false, false, false},
                                    { false, true, true, true, false, true, true, true, false, false, false, false, false, true},

                                    { true, true, false, false, true, false, true, false, false, true, false, false, false, false},
                                    { true, true, false, false, true, false, false, true, true, true, false, false, false, false},
                                    { true, true, false, false, true, false, false, true, true, true, true, false, false, false},
                                    { true, true, false, false, true, false, false, true, false, true, true, false, true, false},
                                    { true, true, false, false, true, false, false, true, true, true, true, false, false, false},
                                    { true, true, false, false, true, false, false, true, true, true, true, true, true, false},
                                    { true, true, false, false, true, false, false, true, false, true, true, false, false, false},


                                    { false, true, true, false, true, true, true, false, false, false, false, false, false, true},
                                    //{ false, false, false, false, false, false, false, true, true, true, true, true, true, true},
                                  };
        }

        public double Probability(bool label)
        {
            double result = 1;
            var labelsCount = _labels.Where(x => x == label).Count();
            for (int i = 0; i < _keywords.Length; i++)
            {

                //if (_labels[i] == label)
                //{
                    double keywordP = 1;
                    double goodKeyWordsCount = 0;
                    for (int j = 0; j < _movies.Length - 1; j++)
                    {
                        if (_matrix[j, i] == _matrix[14, i] && _labels[j] == label)
                            goodKeyWordsCount++;
                    }
                    if (goodKeyWordsCount!=0)
                        keywordP = goodKeyWordsCount / labelsCount;

                    result = result * keywordP;
                //}
            }
            //for (int i = 0; i < _labels.Length; i++)
            //{
            //    if (_labels[i] == label)
            //    {
            //        double keywordP;
            //        double goodKeyWordsCount = 0;
            //        for (int j = 0; j< _movies.Length-1; j++)
            //        {
            //            if (_matrix[i, j])
            //                goodKeyWordsCount++;
            //        }
            //        keywordP = goodKeyWordsCount / labelsCount;

            //        result = result * keywordP;
            //    }
            //}
            return result;
        }

        public void Verdict()
        {
            if (this.Probability(true) > this.Probability(false))
                Console.WriteLine("I like movie with these keywords");
            else
                Console.WriteLine("I do not like movie with these keywords");
        }
    }
}
