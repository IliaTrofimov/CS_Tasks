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