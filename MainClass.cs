using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluator
{
    public static class MainClass
    {
        public static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 5000000; i++)
            {
                var m = new MathEvaluator();
                m.Eval("1+2-3*4/5");
                //MathEvaluator_old.Evaluate("1+2-3*4/5");
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            Console.ReadKey();
        }
    }
}
