using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.AOC2022.Day02
{
    public enum Result
    {
        WIN=0, 
        DRAW=1, 
        LOSS=2 
    }


    /*
     Rock    A , X, 1
     Paper   B , Y, 2
     Sissors C , Z, 3
     */


    public record OutCome(int score, Result result);


    [TestFixture]
    public  class Day02
    {

        private Dictionary<string, OutCome> _lookUpRoundOne = new Dictionary<string, OutCome>()
        {
            {"AX",new OutCome(1+3, Result.DRAW) },
            {"BY",new OutCome(2+3, Result.DRAW) },
            {"CZ",new OutCome(3+3, Result.DRAW) },

            {"AZ",new OutCome(3+0, Result.LOSS) },
            {"CY",new OutCome(2+0, Result.LOSS) },
            {"BX",new OutCome(1+0, Result.LOSS) },

            {"AY",new OutCome(2+6, Result.WIN) },
            {"CX",new OutCome(1+6, Result.WIN) },
            {"BZ",new OutCome(3+6, Result.WIN) },

        };


        /*
         * X=Lose
         * Y=Draw
         * Z=Win
         */


            /*
        Rock    A , X, 1
        Paper   B , Y, 2
        Sissors C , Z, 3
        */

        private Dictionary<string, OutCome> _lookUpRoundTwo = new Dictionary<string, OutCome>()
        {
            {"AX",new OutCome(0+3, Result.LOSS) },
            {"AY",new OutCome(3+1, Result.DRAW) },
            {"AZ",new OutCome(6+2, Result.WIN) },

            {"BX",new OutCome(0+1, Result.LOSS) },
            {"BY",new OutCome(3+2, Result.DRAW) },
            {"BZ",new OutCome(6+3, Result.WIN) },

            {"CX",new OutCome(0+2, Result.LOSS) },
            {"CY",new OutCome(3+3, Result.DRAW) },
            {"CZ",new OutCome(6+1, Result.WIN) },

        };






        [Test]
        public void TestThatLookRoundOneWorks()
        {
            var total = 0;

            total += _lookUpRoundOne["AY"].score;
            total += _lookUpRoundOne["BX"].score;
            total += _lookUpRoundOne["CZ"].score;

            Assert.IsTrue(total == 15);
        }


        [Test]
        public void TestThatLookRoundTwoWorks()
        {
            var total = 0;

            total += _lookUpRoundTwo["AY"].score;
            total += _lookUpRoundTwo["BX"].score;
            total += _lookUpRoundTwo["CZ"].score;

            Assert.IsTrue(total == 12);
        }



        [Test]
        public void GetScoreForTestInputPartOne()
        {
            var sr = new StreamReader("AOC2022\\Day02\\input.txt");

            var total = 0;

            while (!sr.EndOfStream)
            {
                var key=sr.ReadLine().Replace(" ","").Trim();
                total += _lookUpRoundOne[key].score;
            }

            Console.WriteLine($"Total for part 1 is {total}");

            Assert.IsTrue(total >0);
        }
        [Test]
        public void GetScoreForTestInputPartTwo()
        {
            var sr = new StreamReader("AOC2022\\Day02\\input.txt");

            var total = 0;

            while (!sr.EndOfStream)
            {
                var key = sr.ReadLine().Replace(" ", "").Trim();
                total += _lookUpRoundTwo[key].score;
            }

            Console.WriteLine($"Total for part 2 is {total}");

            Assert.IsTrue(total > 0);
        }


    }


}

