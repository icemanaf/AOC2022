using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.AOC2022.Day04
{

    public struct Range
    {

        public int Lower { get; }
        public int Upper { get; }
       
        public Range(int lower,int upper)
        {
            Lower = lower;
            Upper = upper;
        }

        /// <summary>
        /// Checks whether a given range r is contained by the current range
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool FullyContains(Range r)
        {
            return ((r.Lower >= Lower) && (r.Upper <= Upper));
        }

        public bool Overlaps(Range r)
        {
            if (Lower<r.Lower)
            {
                return (Upper >= r.Lower);
            }
            else
            {
                return (r.Upper >= Lower);
            }
            
        }

        public static Range CreateFromString(string input)
        {
            var s = input.Split('-');

            int lower = int.Parse(s[0]);
            int upper = int.Parse(s[1]);

            return new Range(lower, upper);
        }
    }


    [TestFixture]
    public class Day04
    {

        [Test]
        public void TestEnclosingRanges()
        {
            var r1 = new Range(2, 8);
            var r2 = new Range(3, 7);

            //r1 fully contains r2
            Assert.IsTrue(r1.FullyContains(r2));


             r1 = new Range(4, 6);
             r2 = new Range(6, 6);

            //r1 fully contains r2
            Assert.IsTrue(r1.FullyContains(r2));

        }


        [Test]
        public void TestOverLap()
        {
            var r1 = new Range(6, 6);
            var r2 = new Range(4, 6);

            //r1 overlaos r2
            Assert.IsTrue(r1.Overlaps(r2) || r2.Overlaps(r1));

            //does not overlap
             r1 = new Range(2, 4);
             r2 = new Range(6, 8);

            //r1 overlaos r2
            Assert.IsFalse(r1.Overlaps(r2) || r2.Overlaps(r1));

        }



        [Test]
        public void TestRangeCreationFromString()
        {
            var s = "14-38";
            var r = Range.CreateFromString(s);
            Assert.IsTrue(r.Lower == 14 && r.Upper == 38);
        }


        [Test]
        public void GetScoreForTestInputPartOne()
        {
            var sr = new StreamReader("AOC2022\\Day04\\input.txt");

            var total = 0;
            Range r1, r2;

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var s = line.Split(',');
                r1 = Range.CreateFromString(s[0]);
                r2= Range.CreateFromString(s[1]);

                if (r1.FullyContains(r2) || r2.FullyContains(r1))
                    total++;

            }

            Console.WriteLine($"Total for part 1 is {total}");

            Assert.IsTrue(total > 0);
        }


        [Test]
        public void GetScoreForTestInputPartTwo()
        {
            var sr = new StreamReader("AOC2022\\Day04\\input.txt");

            var total = 0;
            Range r1, r2;

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var s = line.Split(',');
                r1 = Range.CreateFromString(s[0]);
                r2 = Range.CreateFromString(s[1]);

                if (r1.Overlaps(r2))
                    total++;

            }

            Console.WriteLine($"Total for part 1 is {total}");

            Assert.IsTrue(total > 0);
        }


    }
}
