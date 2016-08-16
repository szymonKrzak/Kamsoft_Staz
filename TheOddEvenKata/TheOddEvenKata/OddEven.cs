using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheOddEvenKata
{
    public class OddEven
    {
        Random random = new Random();

        public  int NumbersFromRange()
        {
            return random.Next(1,100);
        }

        public string EvenNumber()
        {
           int temp = NumbersFromRange();

           if(temp %2 == 0)
           {
               return "Even";
           }
           else
           {
                return temp.ToString();
           }
        }

        public string OddNumber()
        {
            int temp = NumbersFromRange();

            if (temp % 2 != 0)
            {
                return "Odd";
            }
            else
            {
                return temp.ToString();
            }
        }

        public List<int> PrimeList()
        {
            List<int> list = new List<int>();

            for(int i = 1; i <= 100; i++)
            {
                if(PrimeListBool(i))
                {
                    list.Add(i);
                }
            }

            return list;
        }
        public bool PrimeListBool(int candidate)
        {
            if((candidate & 1) == 0)
            {
                if(candidate == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            for(int i = 3; (i*i) <= candidate; i += 2)
            {
                if((candidate % i) == 0)
                {
                    return false;
                }
            }

            return candidate !=1;
        }

        public int PrimeNumber()
        {
            int temp = NumbersFromRange();
            bool state = false;
            foreach(int item in PrimeList())
            {
                if(item == temp)
                {
                    state = true;
                }
            }

            if (state)
            {
                return temp;
            }
            else
            {
                return 0;
            }
        }

        public string MainFunction(int temp)
        {
            //int temp = NumbersFromRange();

            bool statePrime = false;
            foreach (int item in PrimeList())
            {
                if (item == temp)
                {
                    statePrime = true;
                }
            }

            if(!statePrime)
            {
                if (temp % 2 == 0)
                {
                    return "Even";
                }
                else
                {
                    return "Odd";
                }
            }
            else
            {
                return temp.ToString();
            }
        }
    }
}
