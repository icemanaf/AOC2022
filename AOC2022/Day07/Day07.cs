using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.AOC2022.Day07
{

    public enum NodeType
    {
        File,
        Dir
    }
    public class Node
    {
        public Node Parent { get; set; }
        public NodeType Type { get; set; }
        public int FileSize { get; set; }
        public List<Node> Children { get; set; }

        public string Name { get; set; }

        public int GetSize()
        {
            int result = 0;
            if (Children == null) return 0;
            foreach (Node child in Children)
            {
                if (child.Type == NodeType.File)
                {
                    result += child.FileSize;
                }
                else
                {
                    result += child.GetSize();
                }
            }

            return result;
        }
    }

    [TestFixture]
    public class Day07
    {

        [Test]
        public void TestPartOne()
        {
            var sr = new StreamReader("AOC2022\\Day07\\input.txt");
            bool parse = false;

            var root=new Node();
            root.Parent = null;
            root.Name = "/";
            var dirList = new List<Node>();
            dirList.Add(root);


            Node currentNode=root;

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();

                if (line.StartsWith("$ cd"))
                {
                    var nextDir = line.Split(' ')[2];

                    if (nextDir == "..")
                    {
                        //goback to the parent
                        currentNode = currentNode.Parent;
                    }
                    else
                    {
                        //set node to next directory
                        if (nextDir == "/") 
                        {
                            currentNode = root;
                        }
                        else
                        {
                            currentNode=currentNode.Children.First(x=>x.Name == nextDir && x.Type==NodeType.Dir);
                        }
                    }
                    parse = false;
                    continue;
                }

                if (line == "$ ls")
                {
                    //start adding children until the next command
                    currentNode.Children=new List<Node>();
                    parse = true;
                    continue;
                }

                if (parse)
                {
                    if (line.StartsWith("dir"))
                    {
                        //this is a directory
                        var dirName = line.Split(" ")[1];
                        var n= new Node()
                        {
                            Name = dirName,
                            Type = NodeType.Dir,
                            Parent = currentNode,
                            FileSize = 0,
                            Children = null
                        };
                        currentNode.Children.Add(n);
                        dirList.Add(n);
                  }
                    else
                    {
                        var fileSize=int.Parse(line.Split(" ")[0]);
                        var fileName=line.Split(" ")[1];
                        currentNode.Children.Add(new Node()
                        {
                            Name = fileName,
                            Type = NodeType.File,
                            Children = null,
                            Parent=currentNode,
                            FileSize=fileSize

                        });
                    }
                    //add nodes to current node
                }
            }

            var curatedList = dirList.Where(x => x.GetSize() <= 100000);

            var result=curatedList.Sum(x=>x.GetSize());
            Console.WriteLine($"Result for part1 is ${result}");


            //Doing Part2
            var freeSpaceRequired= 30000000- (70000000 -root.GetSize());

            var candidateDirSize=dirList.Select(x=>x.GetSize()).Where(b=>b>=freeSpaceRequired).Min();
            Console.WriteLine($"Result for part2 is ${candidateDirSize}");

            Assert.IsTrue(dirList.Count>0);

        }
    }
}
