using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Demo
{
    public class Operate
    {
        public static int Add(int a, int b)
        {
            return a + b;
        }

        public int Years { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="years"></param>
        public Operate(int years)
        {
            if (years < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(years), "years值无效");
            }
            Years = years;
        }

        public int ToMonths() => Years * 12;

        protected IEnumerable<object> GetAtomicValues()
        {
            yield return Years;
        }

        public override bool Equals(object obj)
        {
            var b = obj as Operate;
            return Years == b.Years;
        }
    }
}
