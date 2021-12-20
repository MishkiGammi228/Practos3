using System;

namespace Lib_4
{
    public static class Practic
    {
        public static int ColumnOddNumber(int[,] numbers)
        {
            int counter; 
            for (int j = 0; j < numbers.GetLength(1); j++)
            {
                counter = 0;
                for (int i = 0; i < numbers.GetLength(0); i++)
                {
                    if (numbers[i, j] % 2 != 0)
                    {
                        counter++;
                    }
                }
                if (numbers.GetLength(0) == counter)
                {
                    return j+1;
                }
            }
            return 0;
        }
    }
}
