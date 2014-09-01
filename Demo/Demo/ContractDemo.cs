using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class ContractDemo
    {
        private static int constValue = 101;
        static void Foo(int x)
        {
            Contract.Requires(x == -x);
            Contract.Ensures((x & 1) == 0); 
        }

        static void Boo(String s)
        {
            Contract.Requires<ArgumentException>(s.Equals("Hello World!", StringComparison.OrdinalIgnoreCase), "uhh ohh!");
            String yey = new String(s.Take(s.Length).ToArray());
            Contract.Ensures(s.Equals(yey));
        }

        static void Hoo()
        {
            Contract.Ensures(constValue == Contract.OldValue(constValue));
            constValue = 123; //hey!
            constValue = 101;
        }
        static void Main(string[] args)
        {
            //Foo(int.MinValue);
            Boo("Hello World!");
            //Hoo();
        }
    }
}
