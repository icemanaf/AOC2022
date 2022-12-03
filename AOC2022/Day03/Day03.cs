using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.AOC2022.Day03
{

    /*
     * ASCII uppercase from 65 A to 90 Z
     * Lower case from 97 to 122
     * difference of 32
     * create an array of 0- 123 to hold the content of the bag.
     * For the first compartment just set the index value to 1 even for repeated elements.
     * For the second half increment the index value.
     * In the array if there are elements with indexes greater than 2 , that means that character has been duplicated between the compartments
     */


    



    [TestFixture]
    public class Day03
    {
        public int[] GetIndexedArrayfromString(string s)
        {
            int[] ret = new int[123];

            int sLen = s.Length;

            for (int i = 0; i < sLen; i++)
            {
                if (i < sLen / 2)
                {
                    //first compartment
                    var index = s[i];
                    ret[index] = 1;
                }
                else
                {
                    //second compartment
                    var index = s[i];
                    //only increment if the first compartment had that object
                    if (ret[index]>0)
                        ret[index]++;

                }

            }

            return ret;
        }


        public int[] FindTriplicates(string[] input)
        {
            int[] ret = new int[123];

            for(int i=0;i<input.Length;i++)
            {
                int sLen = input[i].Length;

                for (int j=0;j<sLen;j++)
                {
                    var index = input[i][j];

                    if (i == 0)
                    {
                        ret[index] = 1;
                    }
                    else
                    {
                        if (ret[index] == i)
                        {
                            ret[index] ++;
                        }
                    }

                }

               
            }


            return ret;
        }



        public int GenerateScoreFromArray(int[] array,int threshold)
        {
            int total = 0;

            int length = array.Length;

            for (int i = 0; i < length; i++)
            {
                if (array[i] > threshold)
                {
                    if (i > 96)
                    {
                        //lower case
                        total += (i - 96);
                    }
                    else
                    {
                        //upper case
                        total += (i - 38);
                    }
                }
            }


            return total;
        }


        [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp",16)]
        [TestCase("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", 38)]
        [TestCase("PmmdzqPrVvPwwTWBwg", 42)]
        [TestCase("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", 22)]
        [TestCase("ttgJtRGJQctTZtZT", 20)]
        [TestCase("CrZsJsPPZsGzwwsLwLmpwMDw", 19)]
        public void TestScore(string input, int expected)
        {
            var array = GetIndexedArrayfromString(input);
            int score = GenerateScoreFromArray(array,1);
            Assert.IsTrue(score == expected);
        }


        [Test]
        public void GetScoreForTestInputPartOne()
        {
            var sr = new StreamReader("AOC2022\\Day03\\input.txt");

            var total = 0;

            while (!sr.EndOfStream)
            {
                var input = sr.ReadLine();
                total += GenerateScoreFromArray(GetIndexedArrayfromString(input),1);
            }

            Console.WriteLine($"Total for part 1 is {total}");

            Assert.IsTrue(total > 0);
        }


        [Test]
        [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "PmmdzqPrVvPwwTWBwg",18)]
        [TestCase("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", "ttgJtRGJQctTZtZT", "CrZsJsPPZsGzwwsLwLmpwMDw", 52)]
        public void TestTruplicates(string one,string two,string three,int expected)
        {
            string[] input = new string[] { one, two, three };

            var ret = FindTriplicates(input);

            var score = GenerateScoreFromArray(ret, 2);

            Assert.IsTrue(score == expected);
        }



        [Test]
        public void GetScoreForTestInputPartTwo()
        {
            var sr = new StreamReader("AOC2022\\Day03\\input.txt");

            var total = 0;
            string[] inputArray=null;
            int counter = 0;

            while (!sr.EndOfStream)
            {
                if (counter == 0) inputArray = new string[3];
                
                var input = sr.ReadLine();

                inputArray[counter] = input;

                counter++;

                if (counter>2)
                {
                    counter = 0;
                    var retArray = FindTriplicates(inputArray);
                    total += GenerateScoreFromArray(retArray, 2);
                }

                
            }

            Console.WriteLine($"Total for part 2 is {total}");

            Assert.IsTrue(total > 0);
        }




    }
}
