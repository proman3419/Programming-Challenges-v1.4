using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17_Eulerian_path
{
    class Vertex
    {
        public string Name { get; set; }
        public string[] Nodes { get; set; }

        public Vertex(string name)
        {
            Name = name;
        }
    }

    class PathFinder
    {
        int TotalVertices { get { return totalVertices; } set { TotalVertices = totalVertices; } }
        int totalVertices;
        List<Vertex> Vertices { get; set; }

        public PathFinder()
        {
            Vertices = new List<Vertex>();
        }

        public void GetInput()
        {
            Console.WriteLine("How many vertices does the graph have?");
            while (!int.TryParse(Console.ReadLine(), out totalVertices))
                Console.WriteLine("Invalid input, only numbers allowed");
            Console.WriteLine();
            for (int i = 0; i < TotalVertices; i++)
            {
                Console.WriteLine("Name the verticle no " + i);
                Vertices.Add(new Vertex(Console.ReadLine()));
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PathFinder pathFinder = new PathFinder();
            pathFinder.GetInput();

            Console.ReadLine();
        }
    }
}
