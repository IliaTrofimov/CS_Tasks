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