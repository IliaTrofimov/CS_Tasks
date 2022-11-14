using System;
namespace task_4
{
    class Program
    {
        /// <summary>
        /// Возвращает отсортированный по возрастанию поток чисел
        /// </summary>
        /// <param name="inputStream">Поток чисел от 0 до maxValue. Длина потока не превышает миллиарда чисел.</param>
        /// <param name="sortFactor">Фактор упорядоченности потока. Неотрицательное число. Если в потоке встретилось число x, то в нём больше не встретятся числа меньше, чем(x - sortFactor).</param>
        /// <param name="maxValue">Максимально возможное значение чисел в потоке. Неотрицательное число, не превышающее 2000.</param>
        /// <returns>Отсортированный по возрастанию поток чисел.</returns>
        public static IEnumerable<int> Sort(IEnumerable<int> inputStream, int sortFactor, int maxValue)
        {
            SplayTree<int> tree = new SplayTree<int>();           

            foreach (var item in inputStream)
                tree.Add(item);

            return tree;
        }

        static void Main(string[] args)
        {
            var x = new int[] { 1, 0, 2, 5, 4, 6, 9, 7, 8, 3 };
            Console.WriteLine(string.Join(", ", x));
            Console.WriteLine(string.Join(", ", Sort(x, 0, 9)));
        }
    }
}