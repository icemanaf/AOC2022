using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.AOC2022.Day08
{

    public struct Tree
    {
        public short Height;
        /// <summary>
        /// Visibility is set int bitwise order Top Bottom Left right
        /// if a tree is visible only from the top the value would be 0b1000
        /// if a tree is completely hidden this value would be 0b0000
        /// </summary>
        public uint Visibility;

        public int Score;
    }
    [TestFixture]
    public class Day08
    {
        /// <summary>
        /// The array is dimensioned as row and column
        /// </summary>
        /// <returns></returns>
        public Tree[,] LoadTrees(string path, int rows, int cols)
        {
            int row = 0;
            var retval = new Tree[rows, cols];
            var sr = new StreamReader(path);

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var heights = line.ToCharArray();
                for (int i = 0; i < heights.Length; i++)
                {
                    retval[row, i].Height = short.Parse(heights[i].ToString());
                    retval[row, i].Visibility = 0b000;
                }
                row++;
            }


            return retval;

        }

        [Test]
        public void TestLoadingOfTree()
        {
            var tree = LoadTrees("AOC2022\\\\Day08\\\\input.txt", 99, 99);


            Assert.IsTrue(tree[0, 0].Height == 3);
            Assert.IsTrue(tree[0, 98].Height == 1);
            Assert.IsTrue(tree[98, 0].Height == 3);
            Assert.IsTrue(tree[98, 98].Height == 2);

        }


        public void SetVisibilityFromTop(ref Tree[,] forest, int rows, int cols)
        {


            for (int col = 0; col < cols; col++)
            {
                int maxHeight = 0;

                for (int row = 0; row < rows; row++)
                {
                    if (row == 0)
                    {
                        maxHeight = forest[row, col].Height;
                        forest[row, col].Visibility = forest[row, col].Visibility | 0b1000;
                    }
                    else
                    {
                        if (forest[row, col].Height > maxHeight)
                        {
                            maxHeight = forest[row, col].Height;
                            forest[row, col].Visibility = forest[row, col].Visibility | 0b1000;
                        }

                    }


                }
            }


        }

        public void SetVisibilityFromBottom(ref Tree[,] forest, int rows, int cols)
        {


            for (int col = 0; col < cols; col++)
            {
                int maxHeight = 0;
                for (int row = rows - 1; row >= 0; row--)
                {
                    if (row == rows - 1)
                    {
                        maxHeight = forest[row, col].Height;
                        forest[row, col].Visibility = forest[row, col].Visibility | 0b0100;
                    }
                    else
                    {
                        if (forest[row, col].Height > maxHeight)
                        {
                            maxHeight = forest[row, col].Height;
                            forest[row, col].Visibility = forest[row, col].Visibility | 0b0100;
                        }

                    }


                }
            }


        }



        public void SetVisibilityFromLeft(ref Tree[,] forest, int rows, int cols)
        {


            for (int row = 0; row < rows; row++)
            {
                int maxHeight = 0;
                for (int col = 0; col < cols; col++)
                {
                    if (col == 0)
                    {
                        maxHeight = forest[row, col].Height;
                        forest[row, col].Visibility = forest[row, col].Visibility | 0b0010;
                    }
                    else
                    {
                        if (forest[row, col].Height > maxHeight)
                        {
                            maxHeight = forest[row, col].Height;
                            forest[row, col].Visibility = forest[row, col].Visibility | 0b0010;
                        }

                    }


                }
            }


        }


        public void SetVisibilityFromRight(ref Tree[,] forest, int rows, int cols)
        {


            for (int row = 0; row < rows; row++)
            {
                int maxHeight = 0;
                for (int col = cols - 1; col >= 0; col--)
                {
                    if (col == cols - 1)
                    {
                        maxHeight = forest[row, col].Height;
                        forest[row, col].Visibility = forest[row, col].Visibility | 0b0001;
                    }
                    else
                    {
                        if (forest[row, col].Height > maxHeight)
                        {
                            maxHeight = forest[row, col].Height;
                            forest[row, col].Visibility = forest[row, col].Visibility | 0b0001;
                        }

                    }


                }
            }


        }





        [Test]
        public void TestVisibility()
        {
            var trees = LoadTrees("AOC2022\\\\Day08\\\\test.txt", 5, 5);
            SetVisibilityFromTop(ref trees, 5, 5);
            SetVisibilityFromBottom(ref trees, 5, 5);
            SetVisibilityFromLeft(ref trees, 5, 5);
            SetVisibilityFromRight(ref trees, 5, 5);

            int counter = 0;
            //count up the trees that are hidden
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (trees[row, col].Visibility > 0)
                        counter++;
                }
            }

            Assert.IsTrue(counter > 0);
        }


        [Test]
        public void GetPartOneSolution()
        {
            var trees = LoadTrees("AOC2022\\\\Day08\\\\input.txt", 99, 99);
            SetVisibilityFromTop(ref trees, 99, 99);
            SetVisibilityFromBottom(ref trees, 99, 99);
            SetVisibilityFromLeft(ref trees, 99, 99);
            SetVisibilityFromRight(ref trees, 99, 99);

            int counter = 0;
            //count up the trees that are visible
            for (int row = 0; row < 99; row++)
            {
                for (int col = 0; col < 99; col++)
                {
                    if (trees[row, col].Visibility > 0)
                        counter++;
                }
            }

            Console.WriteLine($"Solution for part 1 is {counter}");

            Assert.IsTrue(counter > 0);
        }


        /// <summary>
        /// Given a set of trees and a given tree, calculate it's score.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public void SetTreeScore(ref Tree[,] trees, int row, int col, int rows, int cols)
        {
            var tree = trees[row, col];
            int up = 0, down = 0, left = 0, right = 0;

            //up
            if (row > 0)
            {
                for (int i = row - 1; i >= 0; i--)
                {
                    up++;
                    if (trees[i, col].Height >= tree.Height)
                        break;

                }
            }
            //down
            if (row < rows)
            {
                for (int i = row + 1; i < rows; i++)
                {
                    down++;
                    if (trees[i, col].Height >= tree.Height)
                        break;

                }
            }
            //left
            if (col > 0)
            {
                for (int i = col - 1; i >= 0; i--)
                {
                    left++;
                    if (trees[row, i].Height >= tree.Height)
                        break;

                }
            }

            //right
            if (col < cols)
            {
                for (int i = col + 1; i < cols; i++)
                {
                    right++;
                    if (trees[row, i].Height >= tree.Height)
                        break;
                    ;
                }
            }

            trees[row, col].Score = up * down * left * right;

        }


        [Test]
        public void TestTreeScore()
        {
            var trees = LoadTrees("AOC2022\\\\Day08\\\\test.txt", 5, 5);
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    SetTreeScore(ref trees, row, col, 5, 5);
                }
            }

            int maxScore = 0;

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (trees[row, col].Score > maxScore)
                    {
                        maxScore = trees[row, col].Score;
                    }
                }
            }


            Assert.IsTrue(maxScore == 8);
        }

        [Test]
        public void GetPartTwo()
        {
            var trees = LoadTrees("AOC2022\\\\Day08\\\\input.txt", 99, 99);
            for (int row = 0; row < 99; row++)
            {
                for (int col = 0; col < 99; col++)
                {
                    SetTreeScore(ref trees, row, col, 99, 99);
                }
            }

            int maxScore = 0;

            for (int row = 0; row < 99; row++)
            {
                for (int col = 0; col < 99; col++)
                {
                    if (trees[row, col].Score > maxScore)
                    {
                        maxScore = trees[row, col].Score;
                    }
                }
            }


            Console.WriteLine($"Part 2 solution is {maxScore}");

            Assert.IsTrue(maxScore > 0);

        }



    }
}
