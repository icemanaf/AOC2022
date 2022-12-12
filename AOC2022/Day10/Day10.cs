using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.AOC2022.Day10
{


    public class CPU
    {
        public int Cycles { get; set; }
        public int XRegister { get; set; }
        private Dictionary<int, int> _instructionCyclesX = new Dictionary<int, int>();

        public CPU()
        {
            XRegister = 1;

        }


        public void ProcessInstruction(string instruction)
        {
            int val1 = 0;


            if (instruction == "noop")
            {

                Cycles++;
                _instructionCyclesX.Add(Cycles, XRegister);

            }
            else
            {
                val1 = int.Parse(instruction.Split(" ")[1]);
                Cycles++;
                _instructionCyclesX.Add(Cycles, XRegister);
                Cycles++;
                XRegister = XRegister + val1;
                _instructionCyclesX.Add(Cycles, XRegister);
            }
        }

        public Dictionary<int, int> DumpCore()
        {
            return _instructionCyclesX;
        }

    }

    [TestFixture]
    public class Day10
    {
        [Test]
        public void TestCPU()
        {
            var sr = new StreamReader("AOC2022\\Day10\\test.txt");
            var cpu = new CPU();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                cpu.ProcessInstruction(line);
            }

            var coreDump = cpu.DumpCore();

            var fn = (int x) => coreDump[x - 1] * x;

            var signalStrength = fn(20) + fn(60) + fn(100) + fn(140) + fn(180) + fn(220);

            Assert.IsTrue(signalStrength == 13140);

        }

        [Test]
        public void PartOneSolution()
        {
            var sr = new StreamReader("AOC2022\\Day10\\input.txt");
            var cpu = new CPU();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                cpu.ProcessInstruction(line);
            }

            var coreDump = cpu.DumpCore();

            var fn = (int x) => coreDump[x - 1] * x;

            var signalStrength = fn(20) + fn(60) + fn(100) + fn(140) + fn(180) + fn(220);

            Assert.IsTrue(signalStrength == 10760);

        }


        [Test]
        public void PartTwoSolution()
        {
            var sr = new StreamReader("AOC2022\\Day10\\input.txt");

            var cpu = new CPU();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                cpu.ProcessInstruction(line);
            }

            var coreDump = cpu.DumpCore();

            var fn = (int x) => coreDump[x];

            int x = 1;

            //draw the raster
            // Console.Clear();
            for (int i = 1; i < 241; i++)
            {
                var pixel = ".";


                if (x == (i % 40) - 1 || (x == ((i % 40) - 2)) || x == (i % 40))
                {
                    pixel = "#";
                }
                Console.Write($" {pixel} ");

                if (i % 40 == 0)
                {
                    Console.Write("\r\n");
                }


                x = fn(i);

            }

            /*
             *   #  #  #  #  .  #  #  #  .  .  .  #  #  .  .  #  #  #  .  .  #  .  .  #  .  #  #  #  #  .  .  #  #  .  .  #  .  .  #  . 
                 #  .  .  .  .  #  .  .  #  .  #  .  .  #  .  #  .  .  #  .  #  .  .  #  .  #  .  .  .  .  #  .  .  #  .  #  .  .  #  . 
                 #  #  #  .  .  #  .  .  #  .  #  .  .  .  .  #  .  .  #  .  #  #  #  #  .  #  #  #  .  .  #  .  .  .  .  #  #  #  #  . 
                 #  .  .  .  .  #  #  #  .  .  #  .  #  #  .  #  #  #  .  .  #  .  .  #  .  #  .  .  .  .  #  .  #  #  .  #  .  .  #  . 
                 #  .  .  .  .  #  .  .  .  .  #  .  .  #  .  #  .  .  .  .  #  .  .  #  .  #  .  .  .  .  #  .  .  #  .  #  .  .  #  . 
                 #  .  .  .  .  #  .  .  .  .  .  #  #  #  .  #  .  .  .  .  #  .  .  #  .  #  .  .  .  .  .  #  #  #  .  #  .  .  #  . 
            */


            Assert.IsTrue(true);

        }


    }
}
