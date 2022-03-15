using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    public class TreeNode
    {
        public TreeNode(string data)
        {
            Data = data;
        }

        //данные
        public string Data { get; set; }

        //левая ветка дерева
        public TreeNode Left { get; set; }

        //правая ветка дерева
        public TreeNode Right { get; set; }

        //рекурсивное добавление узла в дерево
        public void Insert(TreeNode node)
        {
            if (node.Data.CompareTo(Data) > 0)
            {
                if (Left == null)
                    Left = node;
                else
                    Left.Insert(node);
            }
            else
            {
                if (Right == null)
                    Right = node;
                else
                    Right.Insert(node);
            }
        }

        //преобразование дерева в отсортированный массив
        public string[] Transform(List<string> elements = null)
        {
            if (elements == null)
                elements = new List<string>();

            if (Left != null)
                Left.Transform(elements);

            elements.Add(Data);

            if (Right != null)
                Right.Transform(elements);

            return elements.ToArray();
        }
    }
    internal class Program
    {
        //метод для обмена элементов массива
        static void Swap(ref string x, ref string y)
        {
            var t = x;
            x = y;
            y = t;
        }

        //сортировка пузырьком
        static string[] BubbleSort(string[] stringArray)
        {
            var len = stringArray.Length;
            for (var i = 0; i < len - 1; i++)
                for (var j = 0; j < len - i - 1; j++)
                    if (stringArray[j].CompareTo(stringArray[j + 1]) > 0)
                        Swap(ref stringArray[j], ref stringArray[j + 1]);

            return stringArray;
        }

        //метод возвращающий индекс опорного элемента
        static int Partition(string[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i].CompareTo(array[maxIndex]) < 0)
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        //быстрая сортировка
        static string[] QuickSort(string[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        static string[] QuickSort(string[] array)
        {
            return QuickSort(array, 0, array.Length - 1);
        }

        static string[] TreeSort(string[] array)
        {
            var treeNode = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++)
                treeNode.Insert(new TreeNode(array[i]));

            return treeNode.Transform();
        }

        //сортировка вставками
        static string[] InsertionSort(string[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                var key = array[i];
                var j = i;
                while ((j > 1) && (array[j - 1].CompareTo(key) > 0))
                {
                    Swap(ref array[j - 1], ref array[j]);
                    j--;
                }

                array[j] = key;
            }

            return array;
        }

        //метод для слияния массивов
        static void Merge(string[] array, int lowIndex, int middleIndex, int highIndex)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new string[highIndex - lowIndex + 1];
            var index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left].CompareTo(array[right]) < 0)
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }

        //сортировка слиянием
        static string[] MergeSort(string[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }

            return array;
        }

        static string[] MergeSort(string[] array)
        {
            return MergeSort(array, 0, array.Length - 1);
        }

        static void Main(string[] args)
        {
            string[] strings = { "123", "sfag", "artea" };
            var sortedArray = MergeSort(strings);
            for(var i = 0; i < sortedArray.Length; i++)
            {
                Console.WriteLine(sortedArray[i]);
            }
            Console.ReadLine();
        }
    }
}
