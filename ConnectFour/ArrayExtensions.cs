using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    internal static class ArrayExtensions
    {
        internal static void SetAllElements<T>(this T[,] obj, T element)
        {
            int rowLenght = obj.GetLength(0);
            int columnLength = obj.GetLength(1);
            
            for (int r = 0; r < rowLenght; r++)
            {
                for (int c = 0; c < columnLength; c++)
                {
                    obj[r, c] = element;
                }
            }
        }

        internal static T[] GetRow<T>(this T[,] obj, int row)
        {
            int columnLength = obj.GetLength(1);
            T[] array = new T[columnLength];
            for (int c = 0; c < columnLength; c++)
            {
                array[c] = obj[row, c];
            }
            return array;
        }

        internal static T[] GetColumn<T>(this T[,] obj, int column)
        {
            int rowLength = obj.GetLength(0);
            T[] array = new T[rowLength];
            for (int r = 0; r < rowLength; r++)
            {
                array[r] = obj[r, column];
            }
            return array;
        }

        internal static T[,] GetSubset<T>(this T[,] obj, int row, int column, int rdim, int cdim)
        {
            T[,] subset = new T[rdim, cdim];

            int rSubset = 0;
            int cSubset = 0;
            for (int r = row; r < row + rdim; r++)
            {
                for (int c = column; c < column + cdim; c++)
                {
                    subset[rSubset, cSubset] = obj[r, c];
                    cSubset++;
                }
                cSubset = 0;
                rSubset++;
            }

            return subset;
        }

        internal static T[] GetPrimaryDiagonal<T>(this T[,] obj)
        {
            int rowLength = obj.GetLength(0);
            int columnLength = obj.GetLength(1);
            List<T> diagonal = new List<T>();

            for (int i = 0; i < rowLength && i < columnLength; i++)
            {
                diagonal.Add(obj[i, i]);
            }

            return diagonal.ToArray();
        }

        internal static T[] GetSecondaryDiagonal<T>(this T[,] obj)
        {
            int rowLength = obj.GetLength(0);
            int columnLength = obj.GetLength(1);
            List<T> diagonal = new List<T>();

            int c = 0;
            for (int i = rowLength - 1; i >= 0 && c < columnLength; i--)
            {
                diagonal.Add(obj[i, c]);
                c++;
            }

            return diagonal.ToArray();
        }
    }
}
