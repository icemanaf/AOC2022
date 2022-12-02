using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2022.Day01
{

    public  record ElfCalories(int elf,List<int> calories);


    

    [TestFixture]
    public class Tests
    {

        public IEnumerable<ElfCalories> ReadDataFile()
        {
            string? line;
            var sr=new StreamReader("AOC2022\\Day01\\input.txt");

            int elf = 1;
            var ec = new ElfCalories(elf, new List<int>());

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                

                if (string.IsNullOrWhiteSpace(line))
                {
                    yield return ec;
                    elf++;
                    ec=new ElfCalories(elf,new List<int>());

                }
                else
                {
                    ec.calories.Add(int.Parse(line));
                }

                if (sr.EndOfStream)
                {
                    yield return ec;
                       
                }
               
            }
        }


        [Test]
        public void FindingTheElfCarryingTheMostCalories()
        {
            int maxElf=0;
            int MaxCaloriesSoFar = 0;

            var elves = ReadDataFile().ToList();

            foreach(var elf in elves)
            {
                
                var sum = elf.calories.Sum();
                if (sum > MaxCaloriesSoFar) 
                {
                    MaxCaloriesSoFar = sum;
                    maxElf = elf.elf;
                }
                    
            }


            Console.WriteLine($"Elf with the max calories of {MaxCaloriesSoFar} is elf {maxElf}");

            Assert.IsTrue(maxElf > 0);
            

        }


        [Test]
        public void FindTheTopThreeElevesCarryingTheMostCalories()
        {
            var TopThreeCalorieSum = ReadDataFile().Select(x => new {Elf= x.elf, Calories=x.calories.Sum() }).OrderByDescending(y=>y.Calories).Take(3).Sum(y=>y.Calories);


            Assert.IsTrue(TopThreeCalorieSum > 0);
            
        }

    }
}