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

        static void Boo(String why)
        {
            Contract.Requires<ArgumentException>(why.Equals("Me", StringComparison.OrdinalIgnoreCase), "why not Me!");
            String yey = why.Take(2).ToString(); //new String(why.Take(2).ToArray());
            Contract.Ensures(why.Equals(yey));
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
            //Boo("me");
            //Hoo();
        }
    }
}
