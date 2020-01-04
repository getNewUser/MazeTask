using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DanskeTask
{
    class Program
    {
        // Every number in a maze is an objectand this is 2D collection of them
        static List<List<Node>> _nodesTwoDimensional = new List<List<Node>>();
        // If computer encounters a deadend it pops the last crossroad it encountered
        // and continues to search for exit the other way
        static Stack _crossroads = new Stack();

        static int _lengthX;
        static int _lengthY;

        static void Main(string[] args)
        {
            GetNodes();
            PrintMap();
            Console.WriteLine("Select starting position: ");
            Console.Write("X: ");
            int x = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("Y: ");
            int y = Convert.ToInt32(Console.ReadLine()) - 1;
            Node startNode = new Node(2, y, x, false);
            _nodesTwoDimensional[y][x].Value = 2;
            Move(startNode);
        }

        static void Move(Node node)
        {
            PrintMap();
            CheckIfExit(node);
            CheckIfCrossroad(node);
            Console.WriteLine("PRESS ANY KEY TO MOVE");
            Console.ReadLine(); // Comment this line to avoid on-click-move

            // Looking for a way to go. Computer can go if path is 0 
            if (_nodesTwoDimensional[node.X - 1][node.Y].Value == 0 )
            {
                _nodesTwoDimensional[node.X - 1][node.Y].Value = 3;
                Move(_nodesTwoDimensional[node.X - 1][node.Y]);
            }
            else if (_nodesTwoDimensional[node.X][node.Y + 1].Value == 0)
            {
                _nodesTwoDimensional[node.X][node.Y + 1].Value = 3;
                Move(_nodesTwoDimensional[node.X][node.Y + 1]);
            }
            else if (_nodesTwoDimensional[node.X + 1][node.Y].Value == 0)
            {
                _nodesTwoDimensional[node.X + 1][node.Y].Value = 3;
                Move(_nodesTwoDimensional[node.X + 1][node.Y]);
            }
            else if (_nodesTwoDimensional[node.X][node.Y - 1].Value == 0)
            {
                _nodesTwoDimensional[node.X][node.Y - 1].Value = 3;
                Move(_nodesTwoDimensional[node.X][node.Y - 1]);
            }
            else
            {
                // this means it's a deadend so pop the last crossroad encountered and keep on looking
                Node lastCrossRoadNode = (Node)_crossroads.Pop();
                Move(lastCrossRoadNode);
            }
        }

        static void CheckIfExit(Node node)
        {
            // Checking if computer came to one of each sides of the maze
            if (node.Y - 1 < 0)
            {
                Console.WriteLine("EXIT WAS FOUND");
                Console.WriteLine("PRESS ANY KEY TO EXIT AND WRITE TRAIL TO FILE");
                Console.ReadLine();
                TrailToTxt();
                Environment.Exit(0);
            }

            else if (node.Y + 2 > _nodesTwoDimensional[0].Count)
            {
                Console.WriteLine("EXIT WAS FOUND");
                Console.WriteLine("PRESS ANY KEY TO EXIT AND WRITE TRAIL TO FILE");
                Console.ReadLine();
                TrailToTxt();
                Environment.Exit(0);
            }
            else if (node.X - 1 < 0)
            {
                Console.WriteLine("EXIT WAS FOUND");
                Console.WriteLine("PRESS ANY KEY TO EXIT AND WRITE TRAIL TO FILE");
                Console.ReadLine();
                TrailToTxt();
                Environment.Exit(0);
            }
            else if (node.X + 2 > _nodesTwoDimensional.Count)
            {
                Console.WriteLine("EXIT WAS FOUND");
                Console.WriteLine("PRESS ANY KEY TO EXIT AND WRITE TRAIL TO FILE");
                Console.ReadLine();
                TrailToTxt();
                Environment.Exit(0);
            }

        }
        static void TrailToTxt()
        {
            String map = "";
            for (int x = 0; x < _lengthY; x++)
            {
                for (int y = 0; y < _lengthX; y++)
                {
                    map += _nodesTwoDimensional[x][y].Value + " ";
                }
                map += "\n";
            }
            Console.WriteLine("TRAIL WAS WRITTEN IN TXT FILE");
            File.WriteAllText(@"C:\Users\vilmi\Downloads\task for candidate\RPAMazeTrail.txt", map);

        }

        static bool CheckIfCrossroad(Node node)
        {
            if (node.IsCrossRoad)
            {
                return true;
            }
            int roadCounter = 0;


            if (_nodesTwoDimensional[node.X - 1][node.Y].Value != 1)
            {
                roadCounter++;
            }
            if (_nodesTwoDimensional[node.X][node.Y + 1].Value != 1)
            {
                roadCounter++;
            }
            if (_nodesTwoDimensional[node.X + 1][node.Y].Value != 1)
            {
                roadCounter++;
            }
            if (_nodesTwoDimensional[node.X][node.Y - 1].Value != 1)
            {
                roadCounter++;
            }


            if (roadCounter >= 3)
            {
                node.IsCrossRoad = true;
                _crossroads.Push(node);
                return true;
            }
            else
            {
                return false;
            }

        }

        static void GetNodes()
        {
            String input = File.ReadAllText(@"C:\Users\vilmi\Downloads\task for candidate\RPAMaze.txt");
            _lengthX = Int32.Parse(input.Substring(0, input.IndexOf(" ")));
            _lengthY = Int32.Parse(input.Substring(input.IndexOf(" "), input.IndexOf("\r\n")));
            String[] text = File.ReadAllLines(@"C:\Users\vilmi\Downloads\task for candidate\RPAMaze.txt");
            text = text.Skip(1).ToArray();

            int[][] list = text
               .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
               .ToArray();


            for (int x = 0; x < _lengthY; x++)
            {
                List<Node> nodesOneDimensional = new List<Node>();
                for (int y = 0; y < _lengthX; y++)
                {
                    Node node = new Node(list[x][y], x, y, false);

                    nodesOneDimensional.Add(node);
                }
                _nodesTwoDimensional.Add(nodesOneDimensional);
            }

        }

        static void PrintMap()
        {
            for (int x = 0; x < _lengthY; x++)
            {
                for (int y = 0; y < _lengthX; y++)
                {
                    Console.Write(_nodesTwoDimensional[x][y].Value + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("============================================================");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
