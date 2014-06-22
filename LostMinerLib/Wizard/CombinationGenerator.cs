namespace TimeSearcher.Wizard
{
    using System;
    using System.Collections.Generic;

    public class CombinationGenerator
    {
        private List<bool[]> combinations;
        private int length;

        public CombinationGenerator(int length)
        {
            this.length = length;
            this.combinations = new List<bool[]>();
            bool[] prev = new bool[length];
            this.CreateCombos(0, prev);
        }

        private void CreateCombos(int level, bool[] prev)
        {
            if (level < this.length)
            {
                bool[] destinationArray = new bool[this.length];
                bool[] flagArray2 = new bool[this.length];
                Array.Copy(prev, destinationArray, level);
                Array.Copy(prev, flagArray2, level);
                destinationArray[level] = true;
                this.CreateCombos(level + 1, destinationArray);
                this.CreateCombos(level + 1, flagArray2);
            }
            else
            {
                this.combinations.Add(prev);
            }
        }

        public void Print()
        {
            foreach (bool[] flagArray in this.combinations)
            {
                for (int i = 0; i < flagArray.Length; i++)
                {
                    if (flagArray[i])
                    {
                        Console.Write("T");
                    }
                    else
                    {
                        Console.Write("F");
                    }
                }
                Console.WriteLine();
            }
        }

        public List<bool[]> List
        {
            get
            {
                return this.combinations;
            }
        }
    }
}

