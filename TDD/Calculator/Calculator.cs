using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a+b;
        }

        public double Div(int a, int b)
        {
            if(b == 0)
            {
                throw new DivideByZeroException();
            }
            else
            {
                return (double)a / b;
            }
        }

        public event EventHandler CalculatedEvent;
        protected virtual void OnCalculated()
        {
            var handler = CalculatedEvent;
            if(handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}

