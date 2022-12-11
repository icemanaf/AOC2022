using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2022.AOC2022.DY09
{
    public struct Point
    {
        public int X; public int Y; public int No;
        public Point(int x, int y, int number)
        {
            X = x;
            Y = y;
            No = number;
        }
        public string GetPosKey()
        {
            return $"{X}-{Y}";
        }
    }


    public class Simulator
    {

        private Dictionary<string, int> _tailPosMap = new Dictionary<string, int>();
        private Point[] _points;
        public Simulator(int tailLength)
        {
            _points = new Point[tailLength + 1];
            for (int i = 0; i < _points.Length; i++)
            {
                _points[i].No = i;
            }
        }



        public int GetUniqueTailVisits()
        {
            return _tailPosMap.Count;
        }


        public Point[] GetPoints()
        {
            return _points;
        }

        public void UpdateTail()
        {

            for (int i = 1; i < _points.Length; i++)
            {

                //model plack behaviour
                int delta_x = Math.Abs(_points[i - 1].X - _points[i].X);
                int delta_y = Math.Abs(_points[i - 1].Y - _points[i].Y);

                if (delta_x > 1 && delta_y > 1)
                {
                    if (_points[i - 1].X > _points[i].X) { _points[i].X++; } else { _points[i].X--; }
                    if (_points[i - 1].Y > _points[i].Y) { _points[i].Y++; } else { _points[i].Y--; }
                }
                else if (delta_x > 1)
                {

                    if (_points[i - 1].X > _points[i].X) { _points[i].X++; } else { _points[i].X--; }

                    _points[i].Y = _points[i - 1].Y;

                }
                else if (delta_y > 1)
                {
                    if (_points[i - 1].Y > _points[i].Y) { _points[i].Y++; } else { _points[i].Y--; }

                    _points[i].X = _points[i - 1].X;
                }

            }




        }

        public void Update(string instruction)
        {
            var ins = instruction.Split(' ');
            var direction = ins[0];
            var distance = int.Parse(ins[1]);
            int counter = 0;

            var _head = _points[0];

            while (counter < distance)
            {
                switch (direction)
                {
                    case "U":
                        _points[0].Y++;
                        break;
                    case "D":
                        _points[0].Y--;
                        break;
                    case "L":
                        _points[0].X--;
                        break;
                    case "R":
                        _points[0].X++;
                        break;
                }

                UpdateTail();

                if (!_tailPosMap.ContainsKey(_points[_points.Length - 1].GetPosKey()))
                {
                    _tailPosMap.Add(_points[_points.Length - 1].GetPosKey(), 1);
                }


                counter++;
            }

        }


    }

    [TestFixture]
    public class Day09
    {

        [Test]
        public void TestSimulation()
        {
            var sim = new Simulator(1);

            var posDict = new Dictionary<string, int>();

            var sr = new StreamReader("AOC2022\\Day09\\test.txt");
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                sim.Update(line);

            }

            var result = sim.GetUniqueTailVisits();

            Assert.IsTrue(result == 13);
        }



        [Test]
        public void TestCommandsPartTwo()
        {
            var sim = new Simulator(9);

            var posDict = new Dictionary<string, int>();


            sim.Update("R 4");
            var points = sim.GetPoints();
            sim.Update("U 4");
            points = sim.GetPoints();



            var result = sim.GetUniqueTailVisits();

            Assert.IsTrue(result == 36);
        }


        [Test]
        public void TestSimulationPartTwo()
        {
            var sim = new Simulator(9);

            var posDict = new Dictionary<string, int>();

            var sr = new StreamReader("AOC2022\\Day09\\test2.txt");
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                sim.Update(line);
                var points = sim.GetPoints();

            }

            var result = sim.GetUniqueTailVisits();

            Assert.IsTrue(result == 36);
        }



        [Test]
        public void GetPartOneSolution()
        {
            var sim = new Simulator(1);

            var posDict = new Dictionary<string, int>();

            var sr = new StreamReader("AOC2022\\Day09\\input.txt");
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                sim.Update(line);

            }

            var visits = sim.GetUniqueTailVisits();
            Console.WriteLine($"solution for part 1 {visits}");

            Assert.IsTrue(visits > 0);
        }

        [Test]
        public void GetPartTwoSolution()
        {
            var sim = new Simulator(9);


            var sr = new StreamReader("AOC2022\\Day09\\input.txt");
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                sim.Update(line);

            }

            var visits = sim.GetUniqueTailVisits();
            Console.WriteLine($"solution for part 2 {visits}");

            Assert.IsTrue(visits > 0);
        }


    }
}
