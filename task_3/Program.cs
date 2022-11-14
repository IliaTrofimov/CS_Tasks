namespace task_3
{
    public static class IEnumberableExtensions
    {
        /// <summary>
        /// <para> Отсчитать несколько элементов с конца </para>
        /// <example> new[] {1,2,3,4}.EnumerateFromTail(2) = (1, ), (2, ), (3, 1), (4,0)</example>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="tailLength">Сколько элеметнов отсчитать с конца  (у последнего элемента tail = 0)</param>
        /// <returns></returns>
        public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
        {
            int length = enumerable.Count(); // O(n) - 1й цикл по списку
            foreach (var item in enumerable) // O(n) - 2й цикл по списку, суммарно O(2n)
            {
                length--;
                if (!tailLength.HasValue || length < tailLength)
                    yield return (item, length);
                else 
                    yield return (item, null);
            }
        }
    }


    class Program
    {
        static void Main()
        {
            var x = new[] { 1, 2, 3, 4 };
            Console.WriteLine(string.Join(", ", x));
            Console.WriteLine(string.Join(", ", x.EnumerateFromTail(null)));
        }
    }
}