using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloBooks
{
    delegate void EinfacherDelegate();
    delegate void DelegateMitPara(string name);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegate
    {
        public HalloDelegate()
        {
            EinfacherDelegate meinDele = EinfacheMethode;
            Action meinDeleAlsAction = EinfacheMethode;
            Action meinDeleAno = delegate () { Console.WriteLine("Ahllo"); };
            Action meinDeleAno2 = () => { Console.WriteLine("Ahllo"); };
            Action meinDeleAno3 = () => Console.WriteLine("Ahllo");

            DelegateMitPara deleMitPara = MethodeMitPara;
            Action<string> deleMitParaAslAction = MethodeMitPara;
            Action<string> deleMitParaAslActionAno = (string x) => { Console.WriteLine(x); };
            Action<string> deleMitParaAslActionAno2 = x => Console.WriteLine(x);

            CalcDelegate calcDele = Minus;
            Func<int, int, long> calcDeleFunc = Sum;
            Func<int, int, long> calcDeleFuncAno = (a, b) => { return a + b; };
            Func<int, int, long> calcDeleFuncAno2 = (a, b) => a + b;

            List<string> lala = null;

            lala.Where(Filter);
            lala.Where(x => x.StartsWith("b"));
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private long Minus(int a, int b)
        {
            return a - b;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void MethodeMitPara(string txt)
        {
            Console.WriteLine($"Hallo {txt}");
        }

        private void EinfacheMethode()
        {
            System.Console.WriteLine("Hallo");
        }
    }
}
