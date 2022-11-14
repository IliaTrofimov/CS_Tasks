## 1. Ломай меня полностью.
Реализуйте метод FailProcess так, чтобы процесс завершался. Предложите побольше различных решений.

```cs
using System;
using System.Diagnostics;
using System.Threading;

namespace task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //FailProcess_StackOverflow();
                //FailProcess_ThreadAbort();
                //FailProcess_ProcessKill();
                //FailProcess_Normal();
            }
            catch { }

            Console.WriteLine("Failed to fail process!");
            Console.ReadKey();
        }


        /// <summary>
        /// Stack Overflow не будет перехвачен, т.к. у приложения не хватит места на стеке для его обработки, ведь всё место мы уже истратили.
        /// Программа будет завершена преждевременно.
        /// </summary>
        static void FailProcess_StackOverflow(long count = 0)
        {
            FailProcess_StackOverflow(count + 1);
            Console.WriteLine($"Done {count}");
        }


        /// <summary>
        /// Thread.Abort() выбросит исключение ThreadAbortException, которое будет отловлено в Main,
        /// однако оно будет автоматически выброшено повторно системой в конце блока catch, из-за чего процесс всё равно завершится преждевременно. 
        /// </summary>
        /// <remarks>Не работает в новых версиях С#, но в .Net Framework 4.8 работает</remarks>
        static void FailProcess_ThreadAbort()
        {
            Console.WriteLine($"FailProcess() was called");
            Thread.CurrentThread.Abort();
        }

        /// <summary>
        /// Завершаем текущий процесс
        /// </summary>
        static void FailProcess_ProcessKill()
        {
            Console.WriteLine($"FailProcess_ProcessKill() was called");
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// Все другие исключения будут обработаны в блоке try-catch в Main
        /// </summary>
        static void FailProcess_Normal()
        {
            int x = 0;
            int y = 1;
            int z = y / x;
        }
    }
}
```

## 2. Операция «Ы».
Что выводится на экран? Измените класс Number так, чтобы на экран выводился результат сложения для
любых значений someValue1 и someValue2.

```cs
using System;
using System.Globalization;


class Program
{
    static readonly IFormatProvider _ifp = CultureInfo.InvariantCulture;
    class Number
    {
        readonly int _number;

        public Number(int number)
        {
            _number = number;
        }

        // Добавим перегрузку оператора +, чтобы можно было складывать два экземпляра Number
        public static Number operator+(Number a, Number b) => new Number(a._number + b._number);

        // Добавим перегрузки оператора +, чтобы можно было складывать экземпляр Number и int
        public static Number operator +(int a, Number b) => new Number(a + b._number);
        public static Number operator +(Number a, int b) => new Number(a._number + b);

        public override string ToString()
        {
            return _number.ToString(_ifp);
        }
    }

    static void Main(string[] args)
    {
        int someValue1 = 10;
        int someValue2 = 5;
        //string result = new Number(someValue1) + someValue2.ToString(_ifp); // старый код

        Number result = new Number(someValue1) + someValue2;                  // исправлено

        Console.WriteLine(result);
        Console.ReadKey();
    }
}
```
Изначально вывоводилось `105`, так как при сложении с любого другого типа со `string` этот тип будет приведён к строке с помощью метода `ToString()`, после чего будет выполнена конкатенация двух строк. В данном случае для `someValue2` был явно вызван `ToString()`, а для `new Number(someValue1)` этот метод вызван неявно.

После исправления вывелось `15`, так как мы сначала сложили числа, а потом привели результат к строке при выводе результата.


## 3. Мне только спросить!
Реализуйте метод по следующей сигнатуре:

```cs
/// <summary>
/// <para> Отсчитать несколько элементов с конца </para>
/// <example> new[] {1,2,3,4}.EnumerateFromTail(2) = (1, ), (2, ), (3, 1), (4,0)</example>
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="enumerable"></param>
/// <param name="tailLength">Сколько элеметнов отсчитать с конца  (у последнего элемента tail = 0)</param>
/// <returns></returns>
public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
```

Возможно ли реализовать такой метод выполняя перебор значений перечисления только 1 раз?

Реализовать такой метод, используя только 1 обход, не получится, т.к. `IEnumerable` не хранит количество элементов и не знает, где находится последний элемент,
а `IEnumerator` может итерировать только вперёд. Поэтому придётся сначала обойти все элементы, чтобы пересчитать их, затем обойти ещё раз, чтобы отсчитать нужно количество. 
Моя реализация:
```cs
public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
{
    int length = enumerable.Count(); // O(n) - 1й цикл по перечислению
    foreach (var item in enumerable) // O(n) - 2й цикл по перечислению, суммарно O(2n)
    {
        length--;
        if (!tailLength.HasValue || length < tailLength)
            yield return (item, length);
        else 
            yield return (item, null);
    }
}
```

## 4. Высший сорт.
Реализуйте метод Sort. Известно, что потребители метода зачастую не будут вычитывать данные до конца.
Оптимально ли Ваше решение с точки зрения скорости выполнения? С точки зрения потребляемой памяти?

```cs
public static IEnumerable<int> Sort(IEnumerable<int> inputStream, int sortFactor, int maxValue)
{
    SplayTree<int> tree = new SplayTree<int>();           
    foreach (var item in inputStream)
        tree.Add(item);
    return tree;
}
```
*Не придумал, как можно использовать параметры* `sortFactor` *и* `maxValue`*, поэтому проигнорирую их.*

Для сортировки будем использовать Splay-дерево. Оно позволяет упорядочивать элементы сразу при добавлении. 
Расходует $O(N)$ памяти и обеспечивает сложность добавления, поиска и удаления $O(logN)$. Также для дерева сделал реализацию интерфейса IEnumerable.

Реализация дерева большая, она находится в репозитории.

## 5. Слон из мухи.
Программа выводит на экран строку «Муха», а затем продолжает выполнять остальной код. Реализуйте
метод TransformToElephant так, чтобы программа выводила на экран строку «Слон», а затем продолжала
выполнять остальной код, не выводя перед этим на экран строку «Муха».

```cs
using System;
using System.Text;

namespace task_5
{
    class Program
    {
        static void Main(string[] args)
        {
            TransformToElephant();
            Console.WriteLine("Муха");
            //... custom application code

            Console.WriteLine("таракан");
            Console.WriteLine(125125);
        }

        static void TransformToElephant()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.SetOut(new MyTextWritter());
        }
    }

    /// <summary>
    /// Этот класс переопределяет стандартные методы TextWriter.WriteLine и TextWriter.Write, чтобы при попытки 
    /// вывести в консоль строку "муха" заменить её на "слон". Остальной вывод не меняется.
    /// </summary>
    public class MyTextWritter : TextWriter
    {
        public override Encoding Encoding => Encoding.GetEncoding(866);

        public override void WriteLine(string? value)
        {
            if (value is not null && value.ToLower() == "муха")
                value = "Cлон";
            
            using (var console = new StreamWriter(Console.OpenStandardOutput(), Encoding))
            {
                console.WriteLine(value);
            } 
        }

        public override void Write(string? value)
        {
            if (value is not null && value.ToLower() == "муха")
                value = "Cлон";

            using (var console = new StreamWriter(Console.OpenStandardOutput(), Encoding))
            {
                console.Write(value);
            }
        }
    }
}
```