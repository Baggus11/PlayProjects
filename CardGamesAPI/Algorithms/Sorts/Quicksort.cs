using System;

namespace CardGamesAPI.Algorithms
{
    public class Quicksort
    {
        //public static void RandomQuicksort(IComparable[] elements, int left, int right)
        public static void Sort(IComparable[] elements, int left, int right)
        {
            int i = left, j = right;
            IComparable pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    IComparable tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                Sort(elements, left, j);
            }

            if (i < right)
            {
                Sort(elements, i, right);
            }
        }
    }
}
