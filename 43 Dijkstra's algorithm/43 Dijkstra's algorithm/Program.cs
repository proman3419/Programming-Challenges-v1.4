using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _43_Dijkstra_s_algorithm
{
    class Node
    {
        public int Id { get; set; }
        public bool Visited { get; set; }
        public int Distance { get; set; }
        public int[] Neighbours { get; set; }
        public int[] Distances { get; set; }

        public Node(int id, int[] neighbours, int[] distances)
        {
            Id = id;
            if (id == 0)
            {
                Visited = true;
                Distance = 0;
            }
            else
            {
                Visited = false;
                Distance = int.MaxValue;
            }
            Neighbours = neighbours;
            Distances = distances;
        }
    }

    class PathFinder
    {
        Node[] Nodes { get; set; }
        int CurrentNode { get; set; }
        int CurrentDistance { get; set; }
        int Destination { get; set; }

        public PathFinder()
        {
            Nodes = new Node[] {
                new Node(0, new int[] { 1, 3 }, new int[] { 1, 2 }),
                new Node(1, new int[] { 0, 2, 3 }, new int[] { 1, 3, 1 }),
                new Node(2, new int[] { 1, 3, 5 }, new int[] { 3, 2, 1 }),
                new Node(3, new int[] { 0, 1, 2, 4 }, new int[] { 2, 1, 2, 3 }),
                new Node(4, new int[] { 3, 5 }, new int[] { 3, 2 }),
                new Node(5, new int[] { 2, 4 }, new int[] { 1, 2 }),
            };
            CurrentNode = 0;
            CurrentDistance = 0;
            Destination = 5;

            while (true)
            {
                UpdateDistance();
                Nodes[CurrentNode].Visited = true;
                if (Nodes[Destination].Visited)
                    break;
                ChangeNode();
            }
            Console.WriteLine("Length of the shortest path: " + Nodes[Destination].Distance);
        }

        void UpdateDistance()
        {
            foreach (int neighbour in Nodes[CurrentNode].Neighbours)
            {
                int neighbourId = 0;
                for (int i = 0; i < Nodes[CurrentNode].Neighbours.Length; i++)
                    if (Nodes[CurrentNode].Neighbours[i] == neighbour)
                        neighbourId = i;

                int newDistance = CurrentDistance + Nodes[CurrentNode].Distances[neighbourId];
                if (newDistance < Nodes[neighbour].Distance)
                {
                    Nodes[neighbour].Distance = newDistance;
                    if (newDistance < CurrentDistance)
                    {
                        CurrentNode = neighbourId;
                        CurrentDistance = newDistance;
                    }
                }
            }
        }

        void ChangeNode()
        {
            int minDistance = int.MaxValue;
            int minDistanceId = 0;
            foreach(Node node in Nodes)
            {
                if (!node.Visited && node.Distance < minDistance)
                {
                    minDistance = node.Distance;
                    minDistanceId = node.Id;
                }
            }
            CurrentNode = minDistanceId;
            CurrentDistance = minDistance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PathFinder pathFinder = new PathFinder();

            Console.ReadLine();
        }
    }
}
