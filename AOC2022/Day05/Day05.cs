using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace AOC2022.AOC2022.Day05
{

    public struct Instruction
    {
        public int NumberToMove { get; }
        public int SourceStack { get; }
        public int DestinationStack { get; }

        public Instruction(int numberToMove, int sourceStack, int destinationStack)
        {
            NumberToMove = numberToMove;
            SourceStack = sourceStack;
            DestinationStack = destinationStack;
        }
    }

    [TestFixture]
    public class Day05
    {
        public Instruction ParseInstruction(string input)
        {
            var regExp = new Regex("\\b\\d+\\b");

            var result = regExp.Matches(input);

            if (result.Count() != 3)
                throw new Exception("Invalid instruction");


            return new Instruction(int.Parse(result[0].Value), int.Parse(result[1].Value), int.Parse(result[2].Value));
        }


        [TestCase("move 2 from 4 to 6", 2, 4, 6)]
        [TestCase("move 20 from 6 to 5", 20, 6, 5)]
        public void TestInstructionParsing(string ins, int expNumber, int expSource, int expDest)
        {
            var ret = ParseInstruction(ins);
            Assert.IsTrue(ret.NumberToMove == expNumber);
            Assert.IsTrue(ret.SourceStack == expSource);
            Assert.IsTrue(ret.DestinationStack == expDest);

        }


        [Test]
        public void TestPartOne()
        {
            string[] initialData = new string[] { "NBDTVGZJ", "SRMDWPF", "VCRSZ", "RTJZPHG", "TCJNDZQF", "NVPWGSFM", "GCVBPQ", "ZBPN", "WPJ" };
            Stack<char>[] stacks = new Stack<char>[initialData.Length];

            for (int i = 0; i < initialData.Length; i++)
            {
                stacks[i] = new Stack<char>();
                foreach (var c in initialData[i])
                {
                    stacks[i].Push(c);
                }
            }

            var sr = new StreamReader("AOC2022\\Day05\\input.txt");

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var ins = ParseInstruction(line);

                int numToMove = ins.NumberToMove;
                while (numToMove > 0)
                {
                    var c = stacks[ins.SourceStack - 1].Pop();
                    stacks[ins.DestinationStack - 1].Push(c);
                    numToMove--;
                }

            }
            char[] chrs = new char[initialData.Length];

            for (int i = 0; i < initialData.Length; i++)
            {
                chrs[i] = stacks[i].Peek();
            }

            string result =new string(chrs);

            Assert.IsTrue(result!=null);
        }

        [Test]
        public void TestPartTwo()
        {
            string[] initialData = new string[] { "NBDTVGZJ", "SRMDWPF", "VCRSZ", "RTJZPHG", "TCJNDZQF", "NVPWGSFM", "GCVBPQ", "ZBPN", "WPJ" };
            Stack<char>[] stacks = new Stack<char>[initialData.Length];
            var stack=new Stack<char>();

            for (int i = 0; i < initialData.Length; i++)
            {
                stacks[i] = new Stack<char>();
                foreach (var c in initialData[i])
                {
                    stacks[i].Push(c);
                }
            }

            var sr = new StreamReader("AOC2022\\Day05\\input.txt");

            while (!sr.EndOfStream)
            {

                var line = sr.ReadLine();
                var ins = ParseInstruction(line);

                stack.Clear();
                int numToMove = ins.NumberToMove;
                while (numToMove > 0)
                {
                    var c = stacks[ins.SourceStack - 1].Pop();
                   stack.Push(c);
                    numToMove--;
                }

                while(stack.Count > 0)
                {
                    stacks[ins.DestinationStack - 1].Push(stack.Pop());
                }

            }
            char[] chrs = new char[initialData.Length];

            for (int i = 0; i < initialData.Length; i++)
            {
                chrs[i] = stacks[i].Peek();
            }

            string result = new string(chrs);

            Assert.IsTrue(result != null);
        }

    }
}
