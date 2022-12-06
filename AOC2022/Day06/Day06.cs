using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace AOC2022.AOC2022.Day06
{

    public class FixedLengthQueue : Queue<char>
    {
        public int Capacity { get; }
        public FixedLengthQueue(int capacity)
        {
            Capacity = capacity;
        }

        public new void Enqueue(char item)
        {
            base.Enqueue(item);

            if (base.Count > Capacity)
                base.Dequeue();
        }

        public bool ItemsAreUnique()
        {

            return !ToArray().GroupBy(x => x).Select(x => new { Item = x.Key, Count = x.Count() }).Any(b => b.Count > 1);

        }
    }



    [TestFixture]
    public class Day06
    {
        [TestCase("ABCD",true)]
        [TestCase("ABCA", false)]
        public void TestUniqueInputs(string s,bool expected)
        {
            var fl = new FixedLengthQueue(4);

            foreach(var c in s)
            {
                fl.Enqueue(c);
            }

            Assert.IsTrue(fl.ItemsAreUnique()== expected);

        }


        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb",7)]
        public void TestMarker(string s,int expected)
        {
            int counter = 0;

            var fl = new FixedLengthQueue(4);

            foreach (var c in s)
            {
                fl.Enqueue(c);
                counter++;

                if (counter >= 4)
                {
                    if (fl.ItemsAreUnique())
                        break;
                }
            }

            Assert.AreEqual(expected, counter);

        }



        [Test]
        public void TestPartOne()
        {
            int counter = 0;

            var fl = new FixedLengthQueue(4);

            var sr=new StreamReader("AOC2022\\Day06\\input.txt");

            while (!sr.EndOfStream)
            {
                var ch = (char)sr.Read();

                fl.Enqueue(ch);
                counter++;

                if (counter >= 4)
                {
                    if (fl.ItemsAreUnique())
                        break;
                }
            }

            Console.WriteLine($"Part 1 marker is ${counter}");

            Assert.IsTrue(counter > 0);
        }




        [Test]
        public void TestPartTwo()
        {
            int counter = 0;

            var fl = new FixedLengthQueue(14);

            var sr = new StreamReader("AOC2022\\Day06\\input.txt");

            while (!sr.EndOfStream)
            {
                var ch = (char)sr.Read();

                fl.Enqueue(ch);
                counter++;

                if (counter >= 14)
                {
                    if (fl.ItemsAreUnique())
                        break;
                }
            }

            Console.WriteLine($"Part 2 marker is ${counter}");

            Assert.IsTrue(counter > 0);
        }
    }
}
