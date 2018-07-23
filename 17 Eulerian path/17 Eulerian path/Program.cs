using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17_Eulerian_path
{
    class Node
    {
        public int Id { get; set; }
        public int Degree { get; set; }
        public List<int> Neighbours { get; set; }

        public Node()
        {
            Neighbours = new List<int>();
        }
    }

    class PathFinder
    {
        Node[] Nodes { get; set; }
        List<int> History { get; set; }

        #region Initialize
        public PathFinder()
        {
            History = new List<int>();
            InitializeNodesArray();
            for (int i = 0; i < Nodes.Length; i++)
                Nodes[i] = new Node();
            InitializeIds();
            GetInput();
            GetDegrees();
        }

        void InitializeIds()
        {
            for (int i = 0; i < Nodes.Length; i++)
                Nodes[i].Id = i;
        }

        void InitializeNodesArray()
        {
            int nodesAmount;
            Console.WriteLine("How many nodes does the graph have?");
            while (!int.TryParse(Console.ReadLine(), out nodesAmount))
                Console.WriteLine("Wrong input");
            Nodes = new Node[nodesAmount];
        }

        void GetInput()
        {
            int exists = -1;
            Console.WriteLine("Answer with 1 if the edge exists, otherwise with 0");
            for (int i = 0; i < Nodes.Length; i++) 
                for (int j = 0; j < Nodes.Length; j++)
                {
                    if (i != j)
                    {
                        Console.Write(i + " --- " + j + " ");

                        while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out exists) || !((exists == 0) || (exists == 1)))
                            Console.WriteLine("\nWrong input");
                        Console.WriteLine();
                        if (exists == 0)
                            continue;
                        else
                            Nodes[i].Neighbours.Add(j);
                    }
                }
        }

        void GetDegrees()
        {
            foreach (Node node in Nodes)
                foreach (int neighbour in node.Neighbours)
                    node.Degree++;
        }
        #endregion

        public void FindAPath()
        {
            DisplayResults(CheckIfPathPossible());
        }

        bool CheckIfPathPossible()
        {
            if (!CheckIfEvenDegrees())
                return false;
            int currentId = 0;
            do
            {
                int neighbourId = ChooseNeighbour(currentId);
                Update(currentId, neighbourId);
                Update(neighbourId, currentId);
                currentId = neighbourId;
            } while (!CheckIfAllNodesVisited());
            return true;
        }

        #region Compositional functions
        void Update(int id, int neighbourId)
        {
            Nodes[id].Degree--;
            Nodes[id].Neighbours.Remove(Nodes[neighbourId].Id);
            History.Add(Nodes[id].Id);
        }

        bool CheckIfEvenDegrees()
        {
            foreach (Node node in Nodes)
                if (node.Degree % 2 != 0)
                    return false;
            return true;
        }

        int ChooseNeighbour(int id)
        {
            List<int> degrees = new List<int>();
            foreach (int neighbour in Nodes[id].Neighbours)
                degrees.Add(Nodes[neighbour].Degree);

            return Nodes[Nodes[id].Neighbours[degrees.IndexOf(degrees.Max())]].Id;
        }

        bool CheckIfAllNodesVisited()
        {
            foreach (Node node in Nodes)
                if (node.Degree > 0)
                    return false;
            return true;
        }
        #endregion

        void DisplayResults(bool pathExists)
        {
            if (pathExists)
            {
                Console.WriteLine("\nGraph has an Eulerian cycle:");
                for (int i = 0; i < History.Capacity; i += 2)
                {
                    try
                    {
                        Console.WriteLine(History[i] + " --> " + History[i + 1]);
                    }
                    catch { }
                }
            }
            else
                Console.WriteLine("Eulerian cycle not possible");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PathFinder pathFinder = new PathFinder();

            pathFinder.FindAPath();

            Console.ReadLine();
        }
    }
}
