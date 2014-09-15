using System;

namespace EnumAndStruct
{
    public enum Week
    {
        Sun = 1,Mon,Tue,Wed,Thu,Fri,Sat
    }

    public struct DataStruct
    {
        public string name;
        public int age;
        public string des;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var rs = Week.Mon;
            Console.WriteLine((int)Week.Mon);

            var dataStruct = new DataStruct {name = "jodif", age = 22, des = "djfiosdjf"};


            Console.Read();
        }
    }
}
