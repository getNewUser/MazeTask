using System;
using System.Collections.Generic;
using System.Text;

namespace DanskeTask
{
    public class Node
    {
        public int Value { get; set; }
        public bool IsCrossRoad { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Node(int value, int x, int y, bool isCrossRoad)
        {
            Value = value;
            X = x;
            Y = y;
            IsCrossRoad = isCrossRoad;
        }
    }
}
