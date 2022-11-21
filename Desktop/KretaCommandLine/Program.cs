using KretaCommandLine.Model;
using System;

namespace KretaCommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            Subject history = new Subject(1, "Történelem");
            history.Equals(null);
        }
    }
}
